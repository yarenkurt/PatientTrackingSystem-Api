using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}