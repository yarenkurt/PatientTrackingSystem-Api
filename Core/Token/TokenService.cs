using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Helpers;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Core.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtOption _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TokenService(JwtOption jwtOption, IHttpContextAccessor httpContextAccessor)
        {
            _jwtOptions = jwtOption;
            _httpContextAccessor = httpContextAccessor;
        }

        public TokenInfo CreateToken(int id, string fullname, PersonType personType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(id, fullname, personType, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TokenInfo
            {
                Token = token,
                RefreshToken = RandomHelper.Mixed(32),
                TokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiration),
                RefreshTokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiration)
            };
        }

        public async Task<List<Claim>> GetUserClaims()
        {
            return new List<Claim>
            {
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier),
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name),
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)
            };
        }


        private JwtSecurityToken CreateJwtSecurityToken(int id, string fullName, PersonType personType, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiration),
                notBefore: DateTime.Now,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Role, ((int) personType).ToString()),
                },
                signingCredentials: signingCredentials
            );
            return jwt;
        }
    }

 
}