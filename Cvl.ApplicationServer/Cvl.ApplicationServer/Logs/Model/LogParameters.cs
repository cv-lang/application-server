using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Tools.Extension;

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
        public LogParameter Param6 => Params.Count > 5 ? Params[5] : null;
        public LogParameter Param7 => Params.Count > 6 ? Params[6] : null;
        public LogParameter Param8 => Params.Count > 7 ? Params[7] : null;

        public override string ToString()
        {
            return string.Join(", ", Params).TruncateLongString(130);
        }
    }
}
