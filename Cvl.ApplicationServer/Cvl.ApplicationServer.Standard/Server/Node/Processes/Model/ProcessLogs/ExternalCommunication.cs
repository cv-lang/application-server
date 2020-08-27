using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    public class ExternalCommunication
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Type { get; internal set; }
        public string Header { get; internal set; }
        public string Content { get; internal set; }
        public string Parameters { get; internal set; }
    }
}
