using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class GeneralViewModel : BaseModel
    {
        public GeneralViewModel()
        {
        }

        public GeneralViewModel(string header, string content)
        {
            Header = header;
            Content = content;
        }

        public string Header { get; set; }
        public string Content { get; set; }

        public bool AutoRefresh { get; set; } = true;
    }
}
