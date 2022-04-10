using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Interfaces;

namespace Cvl.ApplicationServer.Core.Processes
{
    public abstract class BaseProcess : IProcess
    {
        [JsonIgnore] [XmlIgnore] public ProcessData ProcessData { get; set; } = new ProcessData();

        public abstract void LoadProcessState(object? processState);

        public abstract object? GetProcessState();

        public virtual void JobEntry()
        {
        }
    }
}
