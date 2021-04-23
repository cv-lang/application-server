using Cvl.ApplicationServer.Logs.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Storage
{
    public class LogStorageBase
    {     
        public virtual void SaveLogs(LogElement logElement)
        {         

        }

        public virtual LogElement GetLogElement(string uniqueId)
        {
            return null;
        }

        public virtual List<LogElement> GetHeaders()
        {
            return new List<LogElement>();
        }
    }
}
