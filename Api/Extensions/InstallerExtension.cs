using System;
using System.Linq;
using Api.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallAllServices(this IServiceCollection services)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .OrderBy(x => x.Name).Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();
            installers.ForEach(x => x.InstallServices(services));
        }

        
        public static void InstallAllConfigurations(this IApplicationBuilder app)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IConfigureInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .OrderBy(x => x.Name).Select(Activator.CreateInstance).Cast<IConfigureInstaller>().ToList();
            installers.ForEach(x => x.InstallConfigure(app));
        }
    }
}