using System.Linq;
using System.Security.Claims;
using Core.Enums;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetValue(this ClaimsPrincipal claimsPrincipal,string claimTypes)
        {
            return claimsPrincipal?.FindAll(claimTypes).Select(x => x.Value).FirstOrDefault() ?? "";
        }
        
        public static PersonType GetPersonType(this ClaimsPrincipal claimsPrincipal)
        {
            return (PersonType)(claimsPrincipal?.FindAll(ClaimTypes.Role).Select(x => x.Value).FirstOrDefault() ?? "").ToInt();
        }

   
    }
}