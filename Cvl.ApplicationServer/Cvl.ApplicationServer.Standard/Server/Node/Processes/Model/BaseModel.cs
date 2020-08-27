using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class BaseModel
    {
        public long ProcessId { get; set; }

        [Description("Nazwa layoutu widoku")]
        public string Layout { get; set; } = "_Layout";

        [Description("Adres IP Klienta")]
        public string ClientIpAddress { get; set; }
    }
}
