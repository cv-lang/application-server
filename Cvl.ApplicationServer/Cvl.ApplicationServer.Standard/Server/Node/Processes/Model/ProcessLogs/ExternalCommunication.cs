using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    public class ExternalCommunication
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Type { get;  set; }
        public string Header { get;  set; }
        public string Content { get;  set; }
        public string Parameters { get;  set; }
    }
}
