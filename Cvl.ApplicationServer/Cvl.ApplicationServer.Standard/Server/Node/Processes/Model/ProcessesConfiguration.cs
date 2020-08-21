using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessesConfiguration
    {
        public List<ProcessTypeDescription> Processes { get; set; } = new List<ProcessTypeDescription>();
    }
}
