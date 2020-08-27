using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    public class ChildProcess
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Name { get; internal set; }
        public string FullName { get; internal set; }
        public string Description { get; internal set; }
    }
}
