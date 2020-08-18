using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;

namespace CMMS.API.Configuration
{
    internal static class SwaggerExtensions
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            //add the Swagger services
            services.AddOpenApiDocument(document =>
            {
                document.Title = "CMMS API";
                document.Description = "CMMS system as .NET Core REST API CQRS implementation with raw SQL and DDD using Clean Architecture.";
                document.Version = "v1";
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                //      new OperationSecurityScopeProcessor("JWT"));
            });
            return services;
        }

        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {


            app.UseOpenApi();
            app.UseSwaggerUi3();

            return app;
        }
    }
}
