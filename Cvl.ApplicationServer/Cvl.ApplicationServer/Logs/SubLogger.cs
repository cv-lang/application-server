using Cvl.ApplicationServer.Logs.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs
{
    public class SubLogger : Logger
    {      

        public SubLogger(LogElement log)
        {
            this.LogElement = log;
        }

        public override void Dispose()
        {
            //nic w subl loggerze nie robię - zapisywanie jest w rodzicu (loggerze)
        }
    }
}
