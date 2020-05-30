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

[assembly: UserSecretsId("52h8sb06-aaa1-4fff-9f05-3ced1cb623c3")]
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
            services.AddControllers();

            services.AddMemoryCache();

            services.AddSwaggerDocumentation();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<MaintenanceContext>();

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
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            var children = this._configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));

            return ApplicationStartup.Initialize(
                services,
                this._configuration[MaintenanceConnectionString],
                cachingConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerDocumentation();
        }
    }
}
