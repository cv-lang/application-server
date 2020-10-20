using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps
{
    public class ApplicationAcceptData
    {
        public bool AcceptAsPerson { get; set; }
        public bool AcceptAsCompany { get; set; }
        public List<StepAgreement> Agreements { get; set; } = new List<StepAgreement>();
    }
}
