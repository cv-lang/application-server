namespace Cvl.ApplicationServer.Core.Serializers.Interfaces
{
    /// <summary>
    /// Object serializer
    /// </summary>
    public interface ISerializer
    {
        string Serialize(object? obj);

        T? Deserialize<T>(string json);
    }
}
