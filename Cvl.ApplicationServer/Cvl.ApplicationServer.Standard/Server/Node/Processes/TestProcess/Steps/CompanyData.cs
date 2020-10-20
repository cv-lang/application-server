using Cvl.ApplicationServer.Server.Node.Processes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps
{
    public class CompanyData : BaseModel
    {
        public string CompanyIdentificator { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPostCode { get; set; }

    }
}
