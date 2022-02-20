using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Newtonsoft.Json;
using ThreadState = Cvl.ApplicationServer.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Old.Processes.Infrastructure
{
    public class ProcessData
    {
        public ProcessInstanceContainer ProcessInstanceContainer { get; set; }
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;

        [JsonIgnore]
        [XmlIgnore]
        public ApplicationServer.Processes.Threading.ThreadState ThreadStatus
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
