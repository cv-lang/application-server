using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Processes;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Processes.Base
{    
    public class ProcessData
    {
        [XmlIgnore]
        internal ProcessInstanceContainer ProcessInstanceContainer { get; set; } = default!;
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;

        [XmlIgnore]
        public int Step
        {
            get => ProcessInstanceContainer.Step.Step;
            set => ProcessInstanceContainer.Step.Step = value;
        }
    }
}
