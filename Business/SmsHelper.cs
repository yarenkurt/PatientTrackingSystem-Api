using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Core.Sms;
using Core.Sms.Dtos;

namespace Business
{
    public class SmsHelper
    {
        private readonly ISmsService _smsService;

        public SmsHelper(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public async Task<Result> SendAsync(IEnumerable<string> phones, string message)
        {
           return  await _smsService.SendSmsAsync(
                new SmsDto(phones, message,
                    new SmsDto.SmsOptions("5323269293", "5323269293", "Yeditepe")
                )
            );
        }
    }
}