using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Dtos
{
    public class SmsDataDto
    {
        [JsonProperty(PropertyName = "msgBaslik")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "user")] public UserInfoDto User { get; set; }

        [JsonProperty(PropertyName = "tr")] public bool IsTurkish { get; set; }

        [JsonProperty(PropertyName = "msgData")]
        public List<MessageDataDto> Messages { get; set; } = new();
    }
}