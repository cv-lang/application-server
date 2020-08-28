using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessData
    {
        private const string gm = "Dany proces";

        [DataForm(GroupName = gm, Description = "Dane instancji procesu, różne per instancja procesu(proces)")]
        public string ProcessInstanceDescription { get; set; }

        [DataForm(GroupName = gm, Description = "Dane instancji procesu, różne per instancja procesu(proces)")]
        public string ProcessInstanceFullDescription { get; set; }

        [DataForm(GroupName = gm, Description = "Parametr procesu (np. external id, email, NIP, PESEL...)")]
        public string ProcessParameter { get; set; }

        [DataForm(GroupName = gm, Description = "Parametr2 procesu (np. external id, NIP, PESEL...)")]
        public string ProcessParameter2 { get; set; }
    }
}
