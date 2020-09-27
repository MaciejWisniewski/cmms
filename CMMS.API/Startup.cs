using CMMS.API.Configuration;
using CMMS.API.SeedWork;
using CMMS.Application.Configuration;
using CMMS.Application.Configuration.Emails;
using CMMS.Application.Configuration.SmsMessages;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Failures;
using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.Emails;
using CMMS.Infrastructure.SeedWork;
using CMMS.Infrastructure.SmsMessages;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace CMMS.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string MaintenanceConnectionString = "MaintenanceConnectionString";

        public Startup(IWebHostEnvironment env)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddJsonFile($"hosting.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CMMSPolicy", builder =>
            {
                builder
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowAnyHeader()
                       .AllowCredentials();

            }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
            });

            services.AddSignalR();

            services.AddMemoryCache();

            services.AddSwaggerDocumentation();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MaintenanceContext>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = JwtTokens.Issuer,
                        ValidAudience = JwtTokens.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokens.Key))
                    };
                });

            var authorizePolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build();

            services.AddMvc(config => config.Filters.Add(new AuthorizeFilter(authorizePolicy))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddDbContext<MaintenanceContext>(options =>
                {
                    options
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                        .UseSqlServer(_configuration[MaintenanceConnectionString]);
                });

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<NotFoundException>(ex => new NotFoundProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

            var children = _configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            var emailsSettings = _configuration.GetSection("EmailsSettings").Get<EmailsSettings>();
            var emailSender = new EmailSender();
            var smsMessagesSettings = _configuration.GetSection("SmsMessagesSettings").Get<SmsMessagesSettings>();
            var smsMessageSender = new SmsMessageSender();
            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            return ApplicationStartup.Initialize(
                services,
                _configuration[MaintenanceConnectionString],
                emailSender,
                emailsSettings,
                smsMessageSender,
                smsMessagesSettings,
                executionContextAccessor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CMMSPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<FailureHub>("/hub/failures");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseProblemDetails();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerDocumentation();
        }
    }
}
