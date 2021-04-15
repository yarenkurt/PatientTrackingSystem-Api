using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Enums;
using Core.Models;

namespace Core.Token
{
    public interface ITokenService
    {
        TokenInfo CreateToken(int id, string fullname, PersonType personType);
        
        Task<List<Claim>> GetUserClaims();
        
    }
}