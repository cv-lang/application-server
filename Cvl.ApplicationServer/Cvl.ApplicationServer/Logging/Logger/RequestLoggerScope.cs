using Cvl.ApplicationServer.Core.Model.Temporary;
using Cvl.ApplicationServer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Logging.Logger
{
    public class RequestLoggerScope 
    {
        private Guid id = Guid.NewGuid();

        private int currentId = 1;
        private Stack<int?> currentScope;
        public RequestLoggerScope(LogElementService logElementService)
        {
            this._logElementService = logElementService;
            currentScope = new Stack<int?>();
            currentScope.Push(null);
        }

        public long ProcessId { get; set; }
        public Guid RequestId { get; set; } = Guid.NewGuid();

        private List<LogElement> _logs = new List<LogElement>();
        private readonly LogElementService _logElementService;

        internal void AddLog(LogElement log)
        {
            log.ExecutionNumber = currentId++;
            log.ParentNumber = currentScope.Peek();
            log.ProcessId = ProcessId;
            //_logs.Add(log);
            _logElementService.InsertAsync(log).Wait();
        }

        internal async Task FlushAsync()
        {
            
        }
    }
}
