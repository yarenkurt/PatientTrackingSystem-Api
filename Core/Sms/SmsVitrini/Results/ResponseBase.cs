using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Results
{
    public class ResponseBase
    {
        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }

        [JsonProperty(PropertyName = "error")] public string Error { get; set; }
    }
}