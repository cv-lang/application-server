using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Cvl.ApplicationServer.Contexts.Application;
using Cvl.ApplicationServer.Monitoring.Base;

namespace Cvl.ApplicationServer.Monitoring
{
    public class ApplicationMonitor
    {
        private ApplicationContext applicationContext;
        private string applicationModuleName;
        public ApplicationMonitor(ApplicationContext applicationContext, string applicationModuleName)
        {
            this.applicationContext = applicationContext;
            this.applicationModuleName = applicationModuleName;
        }

        #region Logi
        [ThreadStatic]
        private static Logger currentLogger;

        /// <summary>
        /// Funkcja rozpoczyna logowanie funkcji lub requestu w Web
        /// Zwraca loggera który przypisany jest do danej funkcji (automatycznie wpisuje nazwę funkji)
        /// </summary>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        /// <param name="parameters">parametry - będą zapisane w postacie serializowanej</param>
        public Logger StartLogs(string message, string externalId = null, string clientAddress = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var logger = new Logger(this);
            logger.Start(message, externalId, clientAddress,
                memberName, sourceFilePath, sourceLineNumber);   

            //applicationContext.ApplicationMonitoring.AddStartLogging(logger);
            currentLogger = logger;
            return logger;
        }

        /// <summary>
        /// Zwraca logera pod funkcji
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public SubLogger GetSubLogger(
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0,
            string message = "", string externalId = null)
        {
            var logger = currentLogger;            

            var sublogger = new SubLogger(logger);
            sublogger.StartSubmethod($"submetoda - {memberName}",
                memberName, sourceFilePath, sourceLineNumber);

            return sublogger;
        }

        internal void FlushLogger(Logger logger)
        {
            //po zamykaniu głównego 
            //applicationContext.ApplicationMonitoring.FlushLogger(logger);
        }

        #endregion
    }
}
