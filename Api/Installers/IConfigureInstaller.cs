using Microsoft.AspNetCore.Builder;

namespace Api.Installers
{
    public interface IConfigureInstaller
    {
        void InstallConfigure(IApplicationBuilder app);
    }
}