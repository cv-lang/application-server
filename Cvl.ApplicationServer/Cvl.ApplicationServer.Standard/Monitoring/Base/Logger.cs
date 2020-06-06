using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Cvl.ApplicationServer.Monitoring.Base.Enums;
using Cvl.ApplicationServer.Monitoring.Base.Model;
using Cvl.ApplicationServer.Tools;

namespace Cvl.ApplicationServer.Monitoring.Base
{
    /// <summary>
    /// Obiekt odpowiedzialny za logowanie przebiegu wykonywania funkcji
    /// jego logi zapisywane są per logger (zawiera w sobie logii)
    /// powinien być tworzony per funkcja API lub funkcja kontrolera
    /// dzięki temu jedna instancja loggera jest historią wykonania jednego reqesta
    /// </summary>
    public class Logger 
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

        /// <summary>
        /// Data utworzenie logera
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Nazwa metody API, która utworzyła loggera
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// ścieżka do pliku z kodem metody która utworzyła loggera
        /// </summary>
        public string SourceFilePath { get; set; }

        /// <summary>
        /// Linia kodu metody która utworzyła loggera
        /// </summary>
        public int SourceLineNumber { get; set; }

        /// <summary>
        /// Wiadomość przekazana podczas tworzenia loggera
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Zewnętrzny Id, po którym można łączyć wywałania między róznymi systemami
        /// przeważnie ApplicationId
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Parametr 1 ogólnego przeznaczenia
        /// </summary>
        public string Parameter1 { get; set; }

        /// <summary>
        /// Parameter 2 ogólnego przeznaczenia
        /// </summary>
        public string Parameter2 { get; set; }

        /// <summary>
        /// Nazwa aplikacji
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Nazwa modułu/serwisu/kontrolera aplikacji(w zależności od rodzaju aplikacji)
        /// </summary>
        public string ApplicationModuleName { get; set; }
        public string ApplicationLocation { get; set; }
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Funkcja do śledzenia działania programu
        /// </summary>
        /// /// <param name="message">Wiadomość trejsu - zapisywana</param>
        /// <param name="parameters">parametry - będą zapisane w postacie serializowanej</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public void Trace(string message = "",
            object param1 = null,
            object param2 = null,
            object param3 = null,
            object param4 = null,
            object param5 = null,
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.Trace, memberName, sourceFilePath, sourceLineNumber, message, param1, param2, param3, param4, param5);
        }



        /// <summary>
        /// Dodawanie informacji do logów
        /// </summary>
        /// <param name="message">wiadomość informacji</param>
        /// <param name="param1">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="param2">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="param3">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public void Info(string message = "",
            object param1 = null,
            object param2 = null,
            object param3 = null,
            object param4 = null,
            object param5 = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.Info, memberName, sourceFilePath, sourceLineNumber, message, param1, param2, param3, param4, param5);
        }

        /// <summary>
        /// Dodawanie informacji do błędów
        /// </summary>
        /// <param name="message">wiadomość informacji</param>
        /// <param name="param1">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="param2">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="param3">opcjonalny parametr - obiekt który ma zostać zapisany w logach (jako xml)</param>
        /// <param name="memberName">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceFilePath">automatycznie uzupełniany przez kompilator</param>
        /// <param name="sourceLineNumber">automatycznie uzupełniany przez kompilator</param>
        public void Error(string message = "", object param1 = null, object param2 = null, object param3 = null,
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.Error, memberName, sourceFilePath, sourceLineNumber, message, param1, param2, param3);
        }

        public void Error(Exception ex,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.Error, memberName, sourceFilePath, sourceLineNumber, ex.Message, ex.ToString());
        }

        /// <summary>
        /// Loguje błąd biznesowy
        /// </summary>
        /// <param name="message"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void BusinessError(string message = "", object param1 = null, object param2 = null, object param3 = null,
        [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.Error, memberName, sourceFilePath, sourceLineNumber, message, param1, param2, param3);
        }

        /// <summary>
        /// Kończy logowanie - robi flusha
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void End(object ret = null,
            [global::System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [global::System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [global::System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            addLog(LogTypeEnum.End, memberName, sourceFilePath, sourceLineNumber, null, ret, null, null);
            FlushLogs();
            return;
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
            addLog(LogTypeEnum.End, memberName, sourceFilePath, sourceLineNumber, param1: returnValue);

            FlushLogs();

            return returnValue;
        }


        #region tworzenie logów

        private void addLog(LogTypeEnum logType, string memberName, string sourceFilePath,
            int sourceLineNumber, string message = "",
            object param1 = null,
            object param2 = null,
            object param3 = null,
            object param4 = null,
            object param5 = null)
        {
            var log = new LogModel();
            log.TimeStamp = DateTime.Now;
            log.MethodName = memberName;
            log.Message = message;
            log.SourceFilePath = sourceFilePath;
            log.SourceLineNumber = sourceLineNumber;
            log.LogType = logType;

            if (param1 != null)
            {
                addLogParameter(param1, log);
            }

            if (param2 != null)
            {
                addLogParameter(param2, log);
            }

            if (param3 != null)
            {
                addLogParameter(param3, log);
            }

            if (param4 != null)
            {
                addLogParameter(param4, log);
            }

            if (param5 != null)
            {
                addLogParameter(param5, log);
            }

            AddLogModel(log);
        }


        private void addLogParameter(object parameter, LogModel log)
        {
            if (parameter != null)
            {
                var p = new LogParameter();
                if (parameter is Expression<Func<object>> exp1)
                {
                    var compiledParam = exp1.Compile();
                    var val = compiledParam();
                    var body = exp1.Body as MemberExpression;
                    if (body != null)
                    {
                        p.Name = body.Member.Name;
                    }
                    else
                    {
                        p.Name = exp1.ToString();
                    }

                    p.XmlValue = Serializer.SerializeObject(val);
                }
                else
                {
                    p.XmlValue = Serializer.SerializeObject(parameter);
                }

                log.Params.Add(p);
            }
        }

        #endregion

        #region obsługa logów

        public virtual void AddLogModel(LogModel log)
        {
            Logs.Add(log);
        }

        protected virtual void FlushLogs()
        {
            applicationMonitor.FlushLogger(this);
        }

        #endregion

        public string GetLogsString()
        {
            var sb = new StringBuilder();
            foreach (var log in Logs)
            {
                var sbParams = new StringBuilder();
                foreach (var logParameter in log.Params)
                {
                    sbParams.Append($"{logParameter.Name}: {logParameter.XmlValue}");
                }

                sb.Append($"{log.LogType} {log.Message} param:{sbParams.ToString()} {Environment.NewLine}");

            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{TimeStamp} {ApplicationModuleName} {MemberName} {Message} logs:{GetLogsString()}";
        }
    }
}
