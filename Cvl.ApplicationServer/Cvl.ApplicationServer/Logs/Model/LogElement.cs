using Cvl.ApplicationServer.Base.Model;
using Cvl.ApplicationServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Model
{
    /// <summary>
    /// Pojedyńczy element logu
    /// Zapisywany jako lista logów w logerze
    /// </summary>
    //[Table("Log", Schema = "Temporary")]
    public class LogElement : BaseObject
    {        
        public LogElement()
        {
            Params = new LogParameters(this);
        }

        public string UniqueId { get; set; }

        public LogTypeEnum LogType { get; set; }

        /// <summary>
        /// Data utworzenie logera
        /// </summary>
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Moduł
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Nazwa metody API, która utworzyła loggera
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// Wiadomość przekazana podczas tworzenia loggera
        /// </summary>
        public string Message { get; set; }

        public string ExternalId1 { get; set; }
        public string ExternalId2 { get; set; }
        public string ExternalId3 { get; set; }
        public string ExternalId4 { get; set; }

        public long? ParentId { get; set; }
        //[ForeignKey(nameof(ParentId))]
        public virtual ICollection<LogElement> Elements { get; set; } = new List<LogElement>();

        /// <summary>
        /// ścieżka do pliku z kodem metody która utworzyła loggera
        /// </summary>
        public string SourceFilePath { get; set; }

        /// <summary>
        /// Linia kodu metody która utworzyła loggera
        /// </summary>
        public int SourceLineNumber { get; set; }


        #region Parametry
        //public virtual ICollection<LogParameter> Params { get; set; } = new List<LogParameter>();
        public LogParameters Params { get; set; }

        public LogElement AddParameter(object parameterValue, string parameterName = null)
        {

            if (parameterValue is object[] arr)
            {
                if (arr.Length == 0)
                {
                    return this;
                }
            }


            var logParam = new LogParameter()
            {
                Name = parameterName,
                XmlValue = Serializer.SerializeObject(parameterValue),
                StringValue = parameterValue?.ToString(),
               // JsonValue = JsonConvert.SerializeObject(parameterValue)
            };

            Params.Params.Add(logParam);

            return this;
        }

        public LogElement AddExpressionParameter(Expression<Func<object>> exp1)
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
            return $"{UniqueId}: {Message} - {MemberName}";
        }
    }
}
