using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cvl.ApplicationServer.Monitoring.Base.Enums;
using Cvl.ApplicationServer.Tools;

namespace Cvl.ApplicationServer.Monitoring.Base.Model
{
    /// <summary>
    /// Pojedyńczy element logu
    /// Zapisywany jako lista logów w logerze
    /// </summary>
    public class LogModel//LogElement
    {
        public LogTypeEnum LogType { get; set; }
        
        /// <summary>
        /// Data utworzenie logera
        /// </summary>
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// Wiadomość przekazana podczas tworzenia loggera
        /// </summary>
        public string Message { get; set; }

              
        

        #region automatyczne pola lokalizacji kodu: memberName, sourcefilePath

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


        #endregion

        #region Parametry
        public List<LogParameter> Params { get; set; } = new List<LogParameter>();

        public LogModel AddParameter(object parameterValue, string parameterName = null)
        {
            var logParam = new LogParameter() 
            { 
                Name = parameterName, 
                XmlValue= Serializer.SerializeObject(parameterValue) 
            };

            Params.Add(logParam);
       
            return this;
        }

        public LogModel AddExpressionParameter(Expression<Func<object>> exp1)
        {
            var expName = exp1.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            var compiledParam = exp1.Compile();
            var val = compiledParam();
            return AddParameter(val, expName);
        }

        #endregion
        public override string ToString()
        {
            return $"{Message} - {MemberName}";
        }
    }
}
