using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Monitoring.Base.Enums;

namespace Cvl.ApplicationServer.Monitoring.Base.Model
{
    /// <summary>
    /// Pojedyńczy element logu
    /// Zapisywany jako lista logów w logerze
    /// </summary>
    public class LogModel//LogElement
    {
        public LogTypeEnum LogType { get; set; }
        public string Message { get; set; }

        public List<LogParameter> Params { get; set; } = new List<LogParameter>();

        public string MethodName { get; set; }
        public string SourceFilePath { get; set; }
        public int SourceLineNumber { get; set; }
        public string ClientAdress { get; set; }

        public string ApplicationName { get; set; }


        /// <summary>
        /// Środowisko uruchomieniowe - test, preprod, prod
        /// </summary>
        public string RuntimeEnvironment { get; set; }
        public DateTime TimeStamp { get; internal set; }

        public override string ToString()
        {
            return $"{Message} - {MethodName}";
        }
    }
}
