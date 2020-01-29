using System.Text;
using AutoMapper;
using CMMS.Mappers;
using CMMS.Models;
using CMMS.Repositories;
using CMMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CMMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IExclusionTypeRepository, ExclusionTypeRepository>();
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IExclusionRepository, ExclusionRepository>();

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppRoleService, AppRoleService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<IExclusionTypeService, ExclusionTypeService>();
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<IExclusionService, ExclusionService>();

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddControllers();

            services.AddIdentity<AppUser, AppRole>(options => { options.User.RequireUniqueEmail = false; })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureAuthenticationAndAuthorization(services);

            ConfigureAutoMapper(services);

            ConfigureSwagger(services);

            //CORS
            services.AddCors(o => o.AddPolicy("DevelopmentPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                      
            }));
        }

        private static void ConfigureAuthenticationAndAuthorization(IServiceCollection services)
        {
            services.AddAuthentication()
                //.AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = MvsJwtTokens.Issuer,
                        ValidAudience = MvsJwtTokens.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MvsJwtTokens.Key))
                    };
                });

            var authorizePolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();

            services.AddMvc(config =>
                    config.Filters
                        .Add(new AuthorizeFilter(authorizePolicy)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0
                );
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors("DevelopmentPolicy");
            }

            SeedDB.Initialize(
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
