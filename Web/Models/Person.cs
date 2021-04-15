﻿using System;
using Web.Enums;

namespace Web.Models
{
    public class Person 
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