using Cvl.ApplicationServer.Logs.Model;
using Cvl.ApplicationServer.Logs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.ApplicationServer.Logs
{
    public class Logger : IDisposable
    {
        private int logsCounter = 0;

        public Logger()
        { }

       
        #region SubLogger

        public virtual SubLogger GetSubLogger(string message = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            throw new NotImplementedException();
        }

        public virtual void DisposeSubLogger()
        {
        }

        #endregion



        /// <summary>
        /// Lista logów 
        /// </summary>
        public LogElement LogElement { get; set; } = new LogElement();

        #region LogElement

        #endregion

        /// <summary>
        /// Funkcja do śledzenia działania programu
        /// </summary>
        /// /// <param name="message">Wiadomość trejsu - zapisywana</param>
        /// <param name="parameters">parametry - będą zapisane w postacie serializowanej</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public LogElement Trace(string message = "",
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            return addLog(LogTypeEnum.Trace, memberName, sourceFilePath, sourceLineNumber, message);
        }



        /// <summary>
        /// Dodawanie informacji do logów
        /// </summary>
        /// <param name="message">wiadomość informacji</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public LogElement Info(string message = "",
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            return addLog(LogTypeEnum.Info, memberName, sourceFilePath, sourceLineNumber, message);
        }

        /// <summary>
        /// Dodawanie informacji do błędów
        /// </summary>
        /// <param name="message">wiadomość informacji</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public LogElement Error(Exception ex = null, string message = "",
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var log = addLog(LogTypeEnum.Error, memberName, sourceFilePath, sourceLineNumber, $"{message} exeptionMessage:{ex?.Message ?? ""}");
            log.AddParameter(ex?.ToString(), "Exception.ToString()");
            return log;
        }



        /// <summary>
        /// Kończy logowanie - robi flusha
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public LogElement End(object ret = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var log = addLog(LogTypeEnum.End, memberName, sourceFilePath, sourceLineNumber, null);
            return log;
        }

        public LogElement addLog(LogTypeEnum logType, string memberName, string sourceFilePath,
            int sourceLineNumber, string message = "")
        {
            var log = new LogElement();
            log.UniqueId = $"{LogElement.UniqueId}-{logsCounter}";
            log.TimeStamp = DateTime.Now;
            log.MemberName = memberName;
            log.Message = message;
            log.SourceFilePath = sourceFilePath;
            log.SourceLineNumber = sourceLineNumber;
            log.LogType = logType;

            logsCounter++;

            AddLogModel(log);
            return log;
        }

        private void AddLogModel(LogElement log)
        {
            LogElement.Elements.Add(log);
        }


        /// <summary>
        /// Loguje i zwraca podaną wartość
        /// używane do zakończenia logowania w funkcji zwracającej wynik
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnValue"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        public T EndAndReturn<T>(T returnValue = default(T),
                [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var log = addLog(LogTypeEnum.End, memberName, sourceFilePath, sourceLineNumber);
            log.AddParameter(returnValue, "ReturnValue");
            
            return returnValue;
        }

        public Logger AddParameter(object parameterValue, string parameterName = null)
        {

            var first = LogElement.Elements.FirstOrDefault();
            if(first!= null)
            {
                first.AddParameter(parameterValue, parameterName);
            }
            else
            {
                LogElement.AddParameter(parameterValue, parameterName);
            }

            return this;
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException("Logger został użyty niepoprawnie, wymaga Main lub sub loggera");
        }

        public override string ToString()
        {
            return $"({LogElement.Elements.Count}): {string.Join("; ", LogElement.Elements)}";
        }
    }
}
