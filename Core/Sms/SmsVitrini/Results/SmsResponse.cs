using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Results
{
    public class SmsResponse : ResponseBase
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "type")] public string Type { get; set; }

        [JsonProperty(PropertyName = "credits")]
        public int Credit { get; set; }

        [JsonProperty(PropertyName = "ref")] public long ReferenceNumber { get; set; }
    }
}