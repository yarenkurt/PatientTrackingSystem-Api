using System;
using System.Text;
using Core.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Installers.Services
{
    public class JwtInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            var jwtOptions = new JwtOption
            {
                Issuer = "patientTracking.net",
                Audience = "patientTracking.net",
                SecurityKey = "F2peYX7865Yk8wztCxg8jzZGF5yEx4vu4TK4mN8DLtsVpnGa3V5jabYjFhGf",
                AccessTokenExpiration = 15,
                RefreshTokenExpiration = 60*24*10
            };
            
            services.AddSingleton(jwtOptions);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

    }
}