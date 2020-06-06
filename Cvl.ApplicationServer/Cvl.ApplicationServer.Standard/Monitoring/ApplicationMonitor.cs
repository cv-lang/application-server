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
        public Logger StartLogs(Expression<Func<object>> param1 = null,
            Expression<Func<object>> param2 = null,
            Expression<Func<object>> param3 = null,
            object param4 = null,
            object param5 = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0,
            string message = "", string externalId = null)
        {
            var logger = new Logger(this);
            logger.TimeStamp = DateTime.Now;
            logger.ExternalId = externalId;
            logger.MemberName = memberName;
            logger.SourceFilePath = sourceFilePath;
            logger.SourceLineNumber = sourceLineNumber;
            logger.Message = message;
            //logger.ApplicationName = applicationContext.Configuration.ApplicationName;
            logger.ApplicationModuleName = applicationModuleName;
            //logger.ApplicationLocation = applicationContext.Configuration.ApplicationLocation;
            //logger.EnvironmentName = applicationContext.Configuration.EnvironmentName.ToString();
            logger.Parameter1 = param4?.ToString();
            logger.Parameter2 = param5?.ToString();

            logger.Trace($"Start metody - {memberName}", param1, param2, param3, param4, param5,
                memberName, sourceFilePath, sourceLineNumber);

            //applicationContext.ApplicationMonitoring.AddStartLogging(logger);
            currentLogger = logger;
            return logger;
        }

        public SubLogger GetLogger(Expression<Func<object>> param1 = null,
            Expression<Func<object>> param2 = null,
            Expression<Func<object>> param3 = null,
            object param4 = null,
            object param5 = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0,
            string message = "", string externalId = null)
        {
            var logger = currentLogger;

            logger.Trace($"submetoda - {memberName}", param1, param2, param3, param4, param5,
                memberName, sourceFilePath, sourceLineNumber);

            var sublogger = new SubLogger(logger);

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
