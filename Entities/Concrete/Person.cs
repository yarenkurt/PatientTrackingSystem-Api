using System;
using Core.Enums;
using Core.Signatures;

namespace Entities.Concrete
{
    public class Person : IBaseEntity
    {
        public int Id { get; set; }
        public PersonType PersonType { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public string  CreatedUserName { get; set; }
        
    }
}