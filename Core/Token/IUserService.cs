using System.Collections.Generic;
using Core.Enums;

namespace Core.Token
{
    public interface IUserService
    {
        bool IsAuthenticated { get; }
        int PersonId { get; }
        string FullName { get; }
        PersonType PersonType { get; }

        void Check(List<PersonType> personTypes);
    }
}