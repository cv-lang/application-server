using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.UI
{
    public class View
    {
        public string ViewName { get; set; }
        public string ViewDescription { get; set; }
        public object ViewModel { get; set; }

        public List<UIAction> Actions { get; set; } = new List<UIAction>();
    }
}
