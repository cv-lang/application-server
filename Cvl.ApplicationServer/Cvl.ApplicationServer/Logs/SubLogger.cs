using Cvl.ApplicationServer.Logs.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs
{
    public class SubLogger : Logger
    {
        private Logger logger;
        public SubLogger(LogElement log, Logger logger)
        {
            this.LogElement = log;
            this.logger = logger;
            loggerStack.Push(this);
        }

        public override void Dispose()
        {
            //nic w subl loggerze nie robię - zapisywanie jest w rodzicu (loggerze)
            this.logger.DisposeSubLogger();
        }
    }
}
