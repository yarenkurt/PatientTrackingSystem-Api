using System;
using Core.Helpers;
using Xunit;

namespace UnitTest.Core.Helpers
{
    public class HashingHelperTests
    {
        [Fact]
        public void CreatePasswordHash_NotNull_PasswordHashAndPasswordSalt()
        {
            HashingHelper.CreatePasswordHash("124578", out var passwordHash, out var passwordSalt);
            Assert.NotNull(passwordHash);
            Assert.NotNull(passwordSalt);
        }

        [Fact]
        public void CreatePasswordHash_Empty_PasswordNullMessage()
        {
            var ex = Assert.Throws<Exception>(() => HashingHelper.CreatePasswordHash("", out _, out _));
            Assert.Equal("Password cannot be null", ex.Message);
        }

        [Fact]
        public void CreatePasswordHash_Null_PasswordNullMessage()
        {
            var ex = Assert.Throws<Exception>(() => HashingHelper.CreatePasswordHash(null, out _, out _));
            Assert.Equal("Password cannot be null", ex.Message);
        }


        [Fact]
        public void VerifyPasswordHash_Verify_Tru()
        {
            HashingHelper.CreatePasswordHash("124578", out var passwordHash, out var passwordSalt);

            var result = HashingHelper.VerifyPasswordHash("124578", passwordHash, passwordSalt);
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_PasswordCannotBeBlank_PasswordNullMessage()
        {
            var ex = Assert.Throws<Exception>(() => HashingHelper.VerifyPasswordHash("", null, null));
            Assert.Equal("Password cannot be null!", ex.Message);

            ex = Assert.Throws<Exception>(() => HashingHelper.VerifyPasswordHash(null, null, null));
            Assert.Equal("Password cannot be null!",ex.Message);
        }
    }
}