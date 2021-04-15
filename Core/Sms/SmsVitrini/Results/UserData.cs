using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Sms.SmsVitrini.Results
{
    public class UserData
    {
        [JsonProperty(PropertyName = "musteriid")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "musterikodu")]
        public string CustomerCode { get; set; }

        [JsonProperty(PropertyName = "yetkiliadsoyad")]
        public string AuthorizedFullName { get; set; }

        [JsonProperty(PropertyName = "firma")] public string Company { get; set; }

        [JsonProperty(PropertyName = "orjinli")]
        public string Original { get; set; }

        [JsonProperty(PropertyName = "sistem_kredi")]
        public string SystemCredit { get; set; }

        [JsonProperty(PropertyName = "basliklar")]
        public List<string> Titles { get; set; }
    }
}