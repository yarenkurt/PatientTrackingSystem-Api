using System.Threading.Tasks;
using Core.Models;
using Core.Results;

namespace Business.Abstract
{
    public interface IAuthenticationService
    {
        Task<DataResult<TokenInfo>> LoginAsync(LoginDto loginDto);
        
        Task<Result> LogoutAsync(int accountId);

        Task<DataResult<TokenInfo>> RefreshAsync(string refreshToken);

        Task<Result> ForgotPassword(string identityNumber);
        Task<Result> ForgotPasswordDoctorAdmin(string gsm);

    }
}