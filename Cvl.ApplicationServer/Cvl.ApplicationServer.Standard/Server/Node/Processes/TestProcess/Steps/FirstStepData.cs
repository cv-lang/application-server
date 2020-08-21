using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess
{
    public class StepAgreement
    {
        public string Content { get; set; }
        public bool Accepted { get; set; }
        public DateTime AcceptanceDate { get; set; }
    }

    public class FirstStepData
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<string> ProducstsList { get; set; } = new List<string>();
        public string SelectedProduct { get; set; }

        public List<StepAgreement> Agreements { get; set; } = new List<StepAgreement>();
    }
}
