using System.Collections.Generic;
using Core.Enums;
using Core.Models;

namespace Core.Token
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }
        int PersonId { get; }
        string FullName { get; }
        PersonType PersonType { get; }
        UserInfo UserInfo { get; }

    }
}