using System.Threading.Tasks;
using Core.Sms;
using Core.Sms.Dtos;
using Core.Sms.SmsVitrini;
using Xunit;

namespace UnitTest.Core.Plugins
{
    public class SmsVitriniServiceTests
    {
        private readonly ISmsService _smsService;

        public SmsVitriniServiceTests()
        {
            _smsService = new SmsVitriniService();
        }

        [Fact]
        public async Task SendMail_ErrorResult_Response()
        {
            var result = await _smsService.SendSmsAsync(new SmsDto());
            Assert.False(result.Success);
        }
    }
}