using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Validations;
using Core.Aspects.Validation;
using Core.Helpers;
using Core.Models;
using Core.Results;
using Core.Sms;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly ITokenService _tokenService;
        private readonly IRepository<PersonLoginHistory> _loginHistoryRepository;
        private readonly SmsHelper _smsHelper;

        public AuthenticationService(IRepository<Person> repository, ITokenService tokenService,
            IRepository<PersonLoginHistory> loginHistoryRepository, ISmsService smsService, SmsHelper smsHelper)
        {
            _personRepository = repository;
            _tokenService = tokenService;
            _loginHistoryRepository = loginHistoryRepository;
            _smsHelper = smsHelper;
        }

        [ValidationAspect(typeof(LoginValidator))]
        public async Task<DataResult<TokenInfo>> LoginAsync(LoginDto loginDto)
        {
            var user = await _personRepository.GetAsync(u => u.UserName == loginDto.Username);

            if (user == null)
                return new ErrorDataResult<TokenInfo>("User is not found!");

            var verify = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!verify)
            {
                await _loginHistoryRepository.InsertAsync(new PersonLoginHistory
                {
                    PersonId = user.Id,
                    IpAddress = "",
                    IsSuccess = false,
                    Date = DateTime.Now,
                    Message = "Wrong password. Login Failed!;"
                });
                return new ErrorDataResult<TokenInfo>("Password is wrong!");
            }


            var token = _tokenService.CreateToken(user.Id, $"{user.FirstName} {user.LastName}", user.PersonType);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiredDate = token.RefreshTokenExpiration;
            await _personRepository.UpdateAsync(user);

            //Insert a login history record for the logged in user to the personloginhistorytable.
            await _loginHistoryRepository.InsertAsync(new PersonLoginHistory
            {
                PersonId = user.Id,
                IsSuccess = true,
                Date = DateTime.Now,
                Message = $"{user.FirstName} {user.LastName} is successfully logged in!",
                IpAddress = ""
            });

            return new DataResult<TokenInfo>(token, true);
        }


        public async Task<Result> LogoutAsync(int userId)
        {
            var user = await _personRepository.GetAsync(x => x.Id == userId);
            if (user == null)
                return new ErrorDataResult<TokenInfo>("User Not Found!");

            await _loginHistoryRepository.InsertAsync(new PersonLoginHistory
            {
                PersonId = user.Id,
                IpAddress = "",
                IsSuccess = true,
                Date = DateTime.Now,
                Message = "Successfully Logged out!"
            });

            user.RefreshTokenExpiredDate = DateTime.Now.AddDays(-1);
            return await _personRepository.UpdateAsync(user);
        }


        public async Task<DataResult<TokenInfo>> RefreshAsync(string refreshToken)
        {
            var user = await _personRepository.GetAsync(x =>
                x.RefreshToken == refreshToken && x.RefreshTokenExpiredDate > DateTime.Now);

            if (user == null)
                return new ErrorDataResult<TokenInfo>("Refresh Token is Expired!");

            var token = _tokenService.CreateToken(user.Id, $"{user.FirstName} {user.LastName}", user.PersonType);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiredDate = token.RefreshTokenExpiration;
            await _personRepository.UpdateAsync(user);
            return new SuccessDataResult<TokenInfo>(token);
        }


        public async Task<Result> ForgotPassword(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
                return new ErrorResult("identity Number is Wrong!");

            var user = await _personRepository.GetAsync(x => x.UserName == identityNumber);
            if (user == null)
                return new ErrorResult("identity Number is not found!");


            var password = RandomHelper.Mixed(6);
            var result = await _smsHelper.SendAsync(new List<string>{user.Gsm},"Your new password is "+password);
            
            if (!result.Success)
                return result;

            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            result = await _personRepository.UpdateAsync(user);
            return !result.Success ? result : new SuccessResult("Your password is sent to your gsm!");
        }
    }
}