using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Model;
using Newtonsoft.Json;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public class ProcessData
    {
        public ProcessInstanceContainer ProcessInstanceContainer { get; set; }
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;

        [JsonIgnore]
        [XmlIgnore]
        public ThreadState ThreadStatus
        {
            get { return ProcessInstanceContainer.ThreadData.MainThreadState; }
            set { ProcessInstanceContainer.ThreadData.MainThreadState = value; }
        }


        public void SetStep(string stepName, string description, object step)
        {
            ProcessInstanceContainer.Step.Step = Convert.ToInt32(step);
            ProcessInstanceContainer.Step.StepName = stepName;
            ProcessInstanceContainer.Step.StepDescription = description;

            var s = new ProcessStepHistory(ProcessId);
            s.Step.Step = Convert.ToInt32(step);
            s.Step.StepName = stepName;
            s.Step.StepDescription = description;
            
            ProcessInstanceContainer.ProcessStepHistories.Add(s);
        }

        public void SetToJobThread()
        {
            ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForExecution;
        }
    }
}
