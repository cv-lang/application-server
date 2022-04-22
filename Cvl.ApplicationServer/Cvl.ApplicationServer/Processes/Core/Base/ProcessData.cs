using System.Xml.Serialization;
using Cvl.ApplicationServer.Processes.Core.Model;

namespace Cvl.ApplicationServer.Processes.Core.Base
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
