using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Core.Sms.Dtos;

namespace Core.Sms
{
    public interface ISmsService
    {
        
            /// <summary>
            ///     Send Sms
            /// </summary>
            /// <param name="dto"></param>
            /// <returns></returns>
            Task<Result> SendSmsAsync(SmsDto dto);

            /// <summary>
            ///     Sms Title List
            /// </summary>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            /// <returns></returns>
            Task<DataResult<List<string>>> TitlesAsync(string userName, string password);
        
    }
}