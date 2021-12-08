using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Temporary
{
    [Table("LogElement", Schema = "Temporary")]
    public class LogElement : BaseEntity
    {
        public LogElement(DateTime timeStamp, LogLevel level, string host, string source, string logger, string message)
        {
            TimeStamp = timeStamp;
            Level = level;
            Host = host;
            Source = source;
            Logger = logger;
            Message = message;
        }

        public DateTime TimeStamp { get; set; }
        public LogLevel Level { get; set; }
        public string Host { get; set; }

        public string Source { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public int ExecutionNumber { get; internal set; }
        public int? ParentNumber { get; internal set; }
        public long ProcessId { get; internal set; }
    }
}
