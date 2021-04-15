using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers.Services
{
    public class CoreInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.InstallService();
        }
    }
}