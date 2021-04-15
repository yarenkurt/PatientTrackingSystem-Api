using Business.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers.Services
{
    public class BusinessInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.InstallService();
        }
    }
}