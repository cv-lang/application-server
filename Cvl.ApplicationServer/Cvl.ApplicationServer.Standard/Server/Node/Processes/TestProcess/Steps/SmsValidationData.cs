using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Processes.Model;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps
{
    public class SmsValidationData : BaseModel
    {
        public string PhoneNumber { get; set; }
        public string ValidationCodeFromUser { get; set; }
        public string ValidationCode { get; internal set; }
    }
}
