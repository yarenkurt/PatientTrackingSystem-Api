using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers.Services
{
    public class MvcInstaller : IServiceInstaller, IConfigureInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => {   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddCors(options =>
                options.AddDefaultPolicy(builder => 
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            );

        }

        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHttpsRedirection();

        }
    }
}