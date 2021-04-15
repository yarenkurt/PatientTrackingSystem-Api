using Api.Middlewares;
using Excepticon.AspNetCore;
using Excepticon.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers.Services
{
    public class ExceptionInstaller : IServiceInstaller, IConfigureInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddExcepticon();
        }

        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseExcepticon();        
        }
    }
}