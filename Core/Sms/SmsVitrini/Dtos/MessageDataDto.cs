using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Dtos
{
    public class MessageDataDto
    {
        public MessageDataDto(string message, List<string> phones)
        {
            Message = message;
            Phones = phones;
        }

        [JsonProperty(PropertyName = "msg")] public string Message { get; }

        [JsonProperty(PropertyName = "telList")]
        public List<string> Phones { get; }
    }
}