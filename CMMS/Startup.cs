using System.Text;
using CMMS.Models;
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
            services.AddControllers();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<IdentityDbContext>();

            services.AddDbContext<IdentityDbContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

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

            services.AddMvc(config => config.Filters.Add(new AuthorizeFilter(authorizePolicy))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SeedDB.Initialize(
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

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
