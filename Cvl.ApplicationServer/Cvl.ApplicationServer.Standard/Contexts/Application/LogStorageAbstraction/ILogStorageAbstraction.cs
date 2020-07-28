using Cvl.ApplicationServer.Monitoring.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.Application.LogAbstraction
{
    public interface ILogStorageAbstraction
    {
        void FlushLogger(Logger logger);
    }
}
