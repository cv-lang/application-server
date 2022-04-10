using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Processes;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public class ProcessData
    {
        internal ProcessInstanceContainer ProcessInstanceContainer { get; set; } = null!;
        internal LongRunningProcessData LongRunningProcessData = new LongRunningProcessData();
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;

        public int Step
        {
            get => ProcessInstanceContainer.Step.Step;
            set => ProcessInstanceContainer.Step.Step = value;
        }


        
    }
}
