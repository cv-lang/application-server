using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Factory
{
    public class LoggerFactory
    {
        public Logger GetLogger()
        {
            return new Logger(new Logs.Storage.FileStorage());
        }
    }
}
