using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps
{
    public class SmsValidationData
    {
        public string PhoneNumber { get; set; }
        public string ValidationCodeFromUser { get; set; }
        public string ValidationCode { get; internal set; }
    }
}
