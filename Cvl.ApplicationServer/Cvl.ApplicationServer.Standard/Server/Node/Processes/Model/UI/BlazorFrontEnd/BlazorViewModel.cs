using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.UI.BlazorFrontEnd
{
    /// <summary>
    /// Obiekt widoku modelu używany 
    /// </summary>
    public class BlazorViewModel
    {
        public string ProcessName { get; set; }
        public string ProcessViewName { get; set; }

        public long ProcessId { get; set; }

        public object Model { get; set; }

        public IProcessEngine ProcessEngine { get; set; }
    }
}
