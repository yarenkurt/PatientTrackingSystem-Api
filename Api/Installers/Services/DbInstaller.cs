using DataAccess.Contexts.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers.Services
{
    public static class DbInstaller
    {
        public static void InstallDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<PatientTrackingContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("Api")), ServiceLifetime.Singleton);
            
        }
    }
}