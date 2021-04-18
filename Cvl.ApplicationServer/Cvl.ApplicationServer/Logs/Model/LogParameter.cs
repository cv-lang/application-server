using Cvl.ApplicationServer.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Model
{
    /// <summary>
    /// parametry dodane do logu
    /// jeśli ktoś podczas logowania chce zapisać stan parametrów poprzez ich podane
    /// to są zapisywane jako ten obiekt
    /// </summary>
    public class LogParameter : BaseObject
    {
        //[Key]
        public long Id { get; set; }

        public long? LogElementId { get; set; }

        //[ForeignKey(nameof(LogElementId))]
        public virtual LogElement LogElement { get; set; }

        public string Name { get; set; }

        public string XmlValue { get; set; }

        public string StringValue { get; set; }

        public string JsonValue { get; set; }
    }
}
