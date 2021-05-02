using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            if (string.IsNullOrEmpty(password)) throw new Exception("Password cannot be null");

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            if (string.IsNullOrEmpty(password)) throw new Exception("Password cannot be null!");
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
        }
    }
}