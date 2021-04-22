using Cvl.ApplicationServer.Logs.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Factory
{
    public class LoggerFactory
    {
        public LogStorageBase LogStorage { get; set; }
        private string module;

        public LoggerFactory(LogStorageBase logStorage, string module)
        {
            LogStorage = logStorage;
            this.module = module;
        }

        public Logger GetLogger(string externalId1 = null, string external2 = null, string external3 = null, string external4 = null,
            string message = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var logger= new Logger(LogStorage);
            logger.LogElement.ExternalId1 = externalId1;
            logger.LogElement.ExternalId2 = external2;
            logger.LogElement.ExternalId3 = external3;
            logger.LogElement.ExternalId4 = external4;
            logger.LogElement.Module = module;
            logger.Trace($"Start {memberName}");

            return logger;
        }
    }
}
