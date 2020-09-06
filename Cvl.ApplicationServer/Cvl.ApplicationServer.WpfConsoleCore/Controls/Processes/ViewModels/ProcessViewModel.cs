using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.WpfConsole.ViewModels.Base;

namespace Cvl.ApplicationServer.WpfConsole.Controls.Processes.ViewModels
{
    public class ProcessViewModel : ViewModelBase
    {
        public ProcessViewModel(BaseProcess model)
        {
            Process = model;
        }

        public BaseProcess Process { get; set; }
    }
}
