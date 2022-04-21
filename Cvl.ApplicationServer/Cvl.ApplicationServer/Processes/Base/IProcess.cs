using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Base;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface IProcess
    {
        [XmlIgnore] [JsonIgnore]
        ProcessData? ProcessData { get; set; }
        object? GetProcessState();
        void LoadProcessState(object? serializedState);
    }
}
