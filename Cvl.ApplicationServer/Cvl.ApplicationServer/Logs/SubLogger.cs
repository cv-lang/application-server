using Cvl.ApplicationServer.Logs.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs
{
    public class SubLogger : Logger
    {
        private LoggerMain loggerMain;
        public SubLogger(LogElement log, LoggerMain loggerMain)
        {
            this.LogElement = log;
            this.loggerMain = loggerMain;
        }

        public override void Dispose()
        {
            //nic w subl loggerze nie robię - zapisywanie jest w rodzicu (loggerze)
            this.loggerMain.DisposeSubLogger();
        }
    }
}
