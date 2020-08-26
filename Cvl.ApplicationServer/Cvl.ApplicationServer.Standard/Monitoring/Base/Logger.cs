using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cvl.ApplicationServer.Monitoring.Base.Enums;
using Cvl.ApplicationServer.Monitoring.Base.Model;
using Cvl.ApplicationServer.Tools;
using LiteDB;

namespace Cvl.ApplicationServer.Monitoring.Base
{
    /// <summary>
    /// Obiekt odpowiedzialny za logowanie przebiegu wykonywania funkcji
    /// jego logi zapisywane są per logger (zawiera w sobie logii)
    /// powinien być tworzony per funkcja API lub funkcja kontrolera
    /// dzięki temu jedna instancja loggera jest historią wykonania jednego reqesta
    /// </summary>
    public class Logger : IDisposable
    {
        public Logger(ApplicationMonitor applicationMonitor)
        {
            this.applicationMonitor = applicationMonitor;
        }

        /// <summary>
        /// Konstruktor wymagany do desarializacji
        /// </summary>
        public Logger()
        {
        }

        private ApplicationMonitor applicationMonitor;        


        /// <summary>
        /// Lista logów 
        /// </summary>
        public List<LogModel> Logs { get; set; } = new List<LogModel>();
        

        #region Pola logera

        public LogModel StartLog { get; set; }

        /// <summary>
        /// Zewnętrzny Id, po którym można łączyć wywałania między róznymi systemami
        /// przeważnie ApplicationId
        /// </summary>
        public string ExternalId { get; set; }
        public string ClientAddress { get; set; }        

        #region Pola aplikacji

        /// <summary>
        /// Nazwa aplikacji
        /// </summary>
        public string ApplicationName { get; set; }

        internal void Start(string message, string externalId, string clientAddress, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            ExternalId = externalId;
            ClientAddress = clientAddress;
            StartLog = addLog(LogTypeEnum.Start, memberName, sourceFilePath, sourceLineNumber, message);
        }

        internal void StartSubmethod(string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            StartLog = addLog(LogTypeEnum.StartSubMethod, memberName, sourceFilePath, sourceLineNumber, message);
        }

        /// <summary>
        /// Nazwa modułu/serwisu/kontrolera aplikacji(w zależności od rodzaju aplikacji)
        /// </summary>
        public string ApplicationModuleName { get; set; }

        /// <summary>
        /// lokalizacja aplikacji
        /// </summary>
        public string ApplicationLocation { get; set; }

        /// <summary>
        /// Środowisko uruchomieniowe - test, preprod, prod
        /// </summary>
        public string RuntimeEnvironment { get; set; }

        #endregion

        #endregion

        #region Obsługa parametrów fluent

        public Logger AddParameter(object parameterValue, string parameterName = null)
        {
            StartLog.AddParameter(parameterValue, parameterName);
            return this;
        }

        public Logger AddExpressionParameter(Expression<Func<object>> exp1)
        {
            StartLog.AddExpressionParameter(exp1);
            return this;
        }

        #endregion

        /// <summary>
        /// Funkcja do śledzenia działania programu
        /// </summary>
        /// /// <param name="message">Wiadomość trejsu - zapisywana</param>
        /// <param name="parameters">parametry - będą zapisane w postacie serializowanej</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public LogModel Trace(string message = "",
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
        public LogModel Info(string message = "",
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
        public LogModel Error(Exception ex = null, string message = "",
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var log= addLog(LogTypeEnum.Error, memberName, sourceFilePath, sourceLineNumber, message);
            log.AddParameter(ex.ToString(), "Exception");
            return log;
        }        

        

        /// <summary>
        /// Kończy logowanie - robi flusha
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public LogModel End(object ret = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            var log= addLog(LogTypeEnum.End, memberName, sourceFilePath, sourceLineNumber, null);
            FlushLogs();
            return log;
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
            FlushLogs();

            return returnValue;
        }


        #region tworzenie logów

        private LogModel addLog(LogTypeEnum logType, string memberName, string sourceFilePath,
            int sourceLineNumber, string message = "")
        {
            var log = new LogModel();
            log.TimeStamp = DateTime.Now;
            log.MemberName = memberName;
            log.Message = message;
            log.SourceFilePath = sourceFilePath;
            log.SourceLineNumber = sourceLineNumber;
            log.LogType = logType;

            AddLogModel(log);

            return log;
        }
                        

        #endregion

        #region obsługa logów

        public virtual void AddLogModel(LogModel log)
        {
            Logs.Add(log);
        }

        private bool isFlushed = false;
        protected virtual void FlushLogs()
        {
            if (isFlushed == false)
            {
                isFlushed = true;
                applicationMonitor?.FlushLogger(this);
            }
        }

        #endregion

        public string GetLogsString()
        {
            var sb = new StringBuilder();
            foreach (var log in Logs.OrderByDescending(x=>x.TimeStamp).Take(10))
            {
                var sbParams = new StringBuilder();
                foreach (var logParameter in log.Params)
                {
                    sbParams.Append($"{logParameter.Name},");
                }

                sb.Append($"{log.LogType} {log.Message} param:{sbParams.ToString()} {Environment.NewLine}");

            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"logs:{GetLogsString()}";
        }

        public void Dispose()
        {
            FlushLogs();
        }
    }
}
