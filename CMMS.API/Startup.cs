using System;
using System.Linq;
using Hellang.Middleware.ProblemDetails;
using CMMS.API.Configuration;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.SeedWork;
using CMMS.API.SeedWork;
using CMMS.Infrastructure;
using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

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
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddControllers();

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

            var children = _configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));

            return ApplicationStartup.Initialize(
                services,
                _configuration[MaintenanceConnectionString],
                cachingConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CMMSPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
