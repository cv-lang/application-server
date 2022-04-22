using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;

namespace Cvl.ApplicationServer.Core.Tools.Serializers
{
    /// <summary>
    /// Json serializer
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        public virtual string Serialize(object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj);
        }

        public virtual T? Deserialize<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
    }
}
