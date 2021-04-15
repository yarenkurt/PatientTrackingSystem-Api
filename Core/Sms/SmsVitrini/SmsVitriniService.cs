using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Results;
using Core.Sms.Dtos;
using Core.Sms.SmsVitrini.Dtos;
using Core.Sms.SmsVitrini.Results;

namespace Core.Sms.SmsVitrini
{
    public class SmsVitriniService : ISmsService
    {
        public async Task<Result> SendSmsAsync(SmsDto dto)
        {
            try
            {
                return await SendAsync(dto);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public async Task<DataResult<List<string>>> TitlesAsync(string userName, string password)
        {
            var userInfo = new UserInfoDto(userName, password);

            var loginResult = await LoginAsync(userInfo);
            if (!loginResult.Status)
                return new ErrorDataResult<List<string>>(loginResult.Error);

            return new SuccessDataResult<List<string>>(loginResult.UserData?.Titles?.ToList());
        }

        private static async Task<Result> SendAsync(SmsDto dto)
        {
            var userInfo = new UserInfoDto(dto.Options.UserName, dto.Options.Password);

            var loginResult = await LoginAsync(userInfo);
            if (!loginResult.Status)
                return new ErrorResult(loginResult.Error);

            //var phones = dto.Phones.Select(item => item.ToPhone()).Where(phone => phone != "").ToList();
            var phones = dto.Phones.Select(item => item).Where(phone => phone != "").ToList();
            var smsData = new SmsDataDto
            {
                IsTurkish = true,
                User = userInfo,
                Title = dto.Options.Title
            };
            smsData.Messages.Add(new MessageDataDto(dto.Message, phones));
            var data = $"data={smsData.ToJson().ToBase64String()}";
            var request = (HttpWebRequest) WebRequest.Create("http://api.mesajpaneli.com/json_api/");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                await using (var stream = request.GetRequestStream())
                {
                    await using var writer = new StreamWriter(stream);
                    await writer.WriteAsync(data);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null)
                        return new ErrorResult("Response null");

                    var read = await new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEndAsync();
                    var result = read.FromBase64String().FromJson<SmsResponse>();
                    if (!result.Status) return new ErrorResult(result.Error);

                    return new SuccessResult();
                }
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        private static async Task<LoginResponse> LoginAsync(UserInfoDto userInfo)
        {
            var dto = new SmsDataDto {User = userInfo};

            var data = $"data={dto.ToJson().ToBase64String()}";
            var request = (HttpWebRequest) WebRequest.Create("http://api.mesajpaneli.com/json_api/login/");
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                await using (var stream = request.GetRequestStream())
                {
                    await using var writer = new StreamWriter(stream);
                    await writer.WriteAsync(data);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null)
                        return new LoginResponse {Status = false};
                    var read = await new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEndAsync();
                    var result = read.FromBase64String();
                    return result.FromJson<LoginResponse>();
                }
            }
            catch (Exception e)
            {
                return new LoginResponse {Status = false, Error = e.Message};
            }
        }
    }
}