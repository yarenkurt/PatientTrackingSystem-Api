using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object item)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T FromJson<T>(this string item)
        {
            return JsonConvert.DeserializeObject<T>(item);
        }
    }
}