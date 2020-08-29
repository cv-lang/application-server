using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class BaseModel
    {
        private const string gm = "Dane bazowe";

        [DataForm(GroupName = gm, Description = "Id procesu" , Order = 10001)]
        public long ProcessId { get; set; }

        [DataForm(GroupName = gm, Description = "Nazwa layoutu widoku", Order = 10001)]
        public string Layout { get; set; } = "_Layout";

        [DataForm(GroupName = gm, Description = "Adres IP Klienta", Order = 10001)]        
        public string ClientIpAddress { get; set; }
    }
}
