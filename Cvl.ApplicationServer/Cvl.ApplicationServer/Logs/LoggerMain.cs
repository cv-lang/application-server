using System;
using System.Collections.Generic;
using System.Text;
using Cvl.ApplicationServer.Logs.Model;
using Cvl.ApplicationServer.Logs.Storage;

namespace Cvl.ApplicationServer.Logs
{
    public class LoggerMain : Logger
    {
        private readonly LogStorageBase storage;

        public LoggerMain(LogStorageBase storage)
        {
            this.storage = storage;
            loggerStack.Push(this);
        }


        #region SubLogger

        protected Stack<Logger> loggerStack = new Stack<Logger>();

        public override SubLogger GetSubLogger(string message = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var currentLogger = loggerStack.Peek();
            var log = currentLogger.addLog(LogTypeEnum.Trace, memberName, sourceFilePath, sourceLineNumber, message ?? $"Submethod:{memberName}");
            var sub = new SubLogger(log, this);
            loggerStack.Push(sub);
            return sub;
        }
        
        public override void DisposeSubLogger()
        {
            loggerStack.Pop();
        }

        public override void Dispose()
        {
            storage.SaveLogs(LogElement);
        }

        #endregion
    }
}
