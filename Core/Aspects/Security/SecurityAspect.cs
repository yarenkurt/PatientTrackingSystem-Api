using System.Linq;
using Castle.DynamicProxy;
using Core.Enums;
using Core.Exceptions;
using Core.Token;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Security
{
    public class SecurityAspect : MethodInterception
    {
        private readonly PersonType _personType;
        private readonly IUserService _userService;


        public SecurityAspect(PersonType personType)
        {
            Priority = 1;
            _personType = personType;
            _userService = ServiceTool.ServiceProvider.GetService<IUserService>();
        }


        public override void OnBefore(IInvocation invocation)
        {
            if (!_userService.IsAuthenticated || _userService.PersonId == 0)
                throw new AuthenticationException("AuthenticationError");

            if (_personType == PersonType.Null) return;

            var actions = new[] {"GetAll", "Get", "Insert", "Update", "Delete"};
            var action = invocation.Method.Name.Replace("Async", "");

            if (actions.ToList().Contains(action) && _personType != _userService.PersonType)
                throw new AuthenticationException("AuthenticationError");


            if (_personType != _userService.PersonType)
                throw new AuthenticationException("AuthenticationError");
        }
    }
}