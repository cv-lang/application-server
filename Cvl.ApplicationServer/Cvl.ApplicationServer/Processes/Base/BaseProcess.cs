using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Core.Processes
{
    public abstract class BaseProcess : IProcess
    {        
        public ProcessData? ProcessData { get; set; }

        public abstract void LoadProcessState(object? processState);

        public abstract object? GetProcessState();

        public virtual void JobEntry()
        {
        }
    }
}
