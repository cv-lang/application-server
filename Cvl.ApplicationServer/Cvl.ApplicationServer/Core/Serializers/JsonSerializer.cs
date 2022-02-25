using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Newtonsoft.Json;

namespace Cvl.ApplicationServer.Core.Serializers
{
    /// <summary>
    /// Json serializer
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        public virtual string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings();
            var text = JsonConvert.SerializeObject(obj, settings);
            return text;
        }

        public virtual T? Deserialize<T>(string json)
        {
            var settings = new JsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
