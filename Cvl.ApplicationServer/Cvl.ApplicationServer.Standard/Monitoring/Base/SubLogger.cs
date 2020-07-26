using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Monitoring.Base.Model;

namespace Cvl.ApplicationServer.Monitoring.Base
{
    /// <summary>
    /// Logger zwracany dla subfunkcji
    /// </summary>
    public class SubLogger : Logger 
    {
        private Logger logger;

        public SubLogger(Logger logger)
        {
            this.logger = logger;
        }

        public override void AddLogModel(LogModel log)
        {
            this.logger.AddLogModel(log);
        }

        protected override void FlushLogs()
        {
            //w subfunkcja nie robię flusha, robiony jest w głównym loggerze
        }
    }
}
