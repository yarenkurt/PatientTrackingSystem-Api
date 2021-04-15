using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Results
{
    public class LoginResponse : ResponseBase
    {
        [JsonProperty(PropertyName = "userData")]
        public UserData UserData { get; set; }
    }
}