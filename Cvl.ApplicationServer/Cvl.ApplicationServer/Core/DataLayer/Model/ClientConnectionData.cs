namespace Cvl.ApplicationServer.Core.DataLayer.Model
{
    public class ClientConnectionData
    {
        public ClientConnectionData(string clientIpAddress, string clientIpPort, string additionalConnectionData)
        {
            ClientIpAddress = clientIpAddress;
            ClientIpPort = clientIpPort;
            AdditionalConnectionData = additionalConnectionData;
        }

        public string ClientIpAddress { get; set; }

        public string ClientIpPort { get; set; }

        public string AdditionalConnectionData { get; set; }
        public virtual string GetClientConnectionData()
        {
            return AdditionalConnectionData;
        }
    }
}
