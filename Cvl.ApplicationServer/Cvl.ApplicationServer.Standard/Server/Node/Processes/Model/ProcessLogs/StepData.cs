using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    public class StepData
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string StepName { get; set; }
        public string StepDescription { get; set; }        
    }
}
