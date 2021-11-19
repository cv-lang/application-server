using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Threading
{
    public class ThreadTypeAttribute : Attribute
    {
        public ThreadType ThreadType { get; set; }
    }
}
