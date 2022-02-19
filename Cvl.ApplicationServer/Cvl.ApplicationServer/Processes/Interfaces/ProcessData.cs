using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Model.Processes;
using Newtonsoft.Json;

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


        public void SetStep(string stepName, string description, Enum step)
        {
            ProcessInstanceContainer.Step.Step = Convert.ToInt32(step);
            ProcessInstanceContainer.Step.StepName = stepName;
            ProcessInstanceContainer.Step.StepDescription = description;
        }
    }
}
