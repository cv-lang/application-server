using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Model
{
    /// <summary>
    /// Kolekcja parametrów
    /// </summary>
    public class LogParameters
    {
        private LogElement logElement;

        public LogParameters(LogElement logElement)
        {
            this.logElement = logElement;
        }

        public LogParameters()
        { }

        public string Id => logElement?.UniqueId + "params";

        public List<LogParameter> Params { get; set; } = new List<LogParameter>();

        public LogParameter Param1 => Params.Count > 0 ? Params[0] : null;
        public LogParameter Param2 => Params.Count > 1 ? Params[1] : null;
        public LogParameter Param3 => Params.Count > 2 ? Params[2] : null;
        public LogParameter Param4 => Params.Count > 3 ? Params[3] : null;
        public LogParameter Param5 => Params.Count > 4 ? Params[4] : null;
    }
}
