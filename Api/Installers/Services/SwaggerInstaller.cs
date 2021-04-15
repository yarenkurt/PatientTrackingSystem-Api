using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api.Installers.Services
{
    public class SwaggerInstaller : IServiceInstaller, IConfigureInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PatientTracker API",
                    Version = "v1",
                    Description = ".NET Core 6.0.100"
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization by using Bearer Scheme. For example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement {{securitySchema, new[] {"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
                //c.OperationFilter<RequiredHeaderParameter>();
            });
        }
        

        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Service");
                //c.DefaultModelExpandDepth(2);
                //c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                //c.DisplayOperationId();
                //c.DisplayRequestDuration();
                //c.DocExpansion(DocExpansion.List);
                c.DocExpansion(DocExpansion.None);
                //c.EnableDeepLinking();
                //c.EnableFilter();
                //c.MaxDisplayedTags(5);
                //c.ShowExtensions();
                //c.EnableValidator();
                //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
            });
        }
    }
}