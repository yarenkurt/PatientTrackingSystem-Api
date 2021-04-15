using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Dtos
{
    public class UserInfoDto
    {
        public UserInfoDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [JsonProperty(PropertyName = "name")] public string UserName { get; set; }

        [JsonProperty(PropertyName = "pass")] public string Password { get; set; }
    }
}