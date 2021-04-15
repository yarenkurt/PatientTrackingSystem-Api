using Core.Sms;
using Core.Sms.SmsVitrini;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallService(this IServiceCollection services)
        {
            services.AddSingleton<ISmsService, SmsVitriniService>();
        }

    }
}