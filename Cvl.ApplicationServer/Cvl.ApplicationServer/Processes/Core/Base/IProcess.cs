using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Base;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface IProcess
    {        
        ProcessData? ProcessData { get; set; }       
    }

    public interface IStateProcess
    {
        object? GetProcessState();
        void LoadProcessState(object? serializedState);
    }
}
