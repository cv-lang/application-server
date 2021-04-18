using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Model
{
    public enum LogTypeEnum
    {
        Start,
        Info,
        Trace,
        BusinessError,
        Error,
        End,
        StartSubMethod,
        EndSubMethod
    }
}
