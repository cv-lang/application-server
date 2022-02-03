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
        internal ProcessInstanceContainer ProcessInstanceContainer { get; set; }
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;

        [JsonIgnore]
        [XmlIgnore]
        public ApplicationServer.Processes.Threading.ThreadState ThreadStatus
        {
            get { return ProcessInstanceContainer.ThreadData.MainThreadState; }
            set { ProcessInstanceContainer.ThreadData.MainThreadState = value; }
        }
    }
}
