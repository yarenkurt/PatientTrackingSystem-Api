using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Core.Enums;
using Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace Core.Token
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        public int PersonId  => _httpContextAccessor?.HttpContext?.User?.GetValue(ClaimTypes.NameIdentifier).ToInt() ?? 0;
        public string FullName => _httpContextAccessor?.HttpContext?.User?.GetValue(ClaimTypes.Name) ?? "";
        public PersonType PersonType =>  _httpContextAccessor?.HttpContext?.User?.GetPersonType() ?? PersonType.Patient;
        
        public void Check(List<PersonType> personTypes)
        {
            if (personTypes.All(x => x != PersonType))
                throw new AuthenticationException("FORBIDDEN");
        }
    }
}