using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Test
{

    
    public class TestProcess : BaseProcess
    {
        public int TestMethod1(int i)
        {
            return i;
        }
    }
}
