using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;

namespace Cvl.ApplicationServer.Core.Tools.Serializers
{
    /// <summary>
    /// Serialize full object wiht type
    /// </summary>
    public class FullSerializer : IFullSerializer
    {
        public virtual string Serialize(object? obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj);
        }

        public virtual T? Deserialize<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
    }
}
