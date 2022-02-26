using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Newtonsoft.Json;

namespace Cvl.ApplicationServer.Core.Serializers
{
    /// <summary>
    /// Serialize full object wiht type
    /// </summary>
    public class FullSerializer : IFullSerializer
    {
        public virtual string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var text = JsonConvert.SerializeObject(obj, settings);
            return text;
        }

        public virtual T? Deserialize<T>(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
