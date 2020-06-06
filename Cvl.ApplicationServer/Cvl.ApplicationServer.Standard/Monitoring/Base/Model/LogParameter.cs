using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Monitoring.Base.Model
{
    /// <summary>
    /// parametry dodane do logu
    /// jeśli ktoś podczas logowania chce zapisać stan parametrów poprzez ich podane
    /// to są zapisywane jako ten obiekt
    /// </summary>
    public class LogParameter
    {
        public string Name { get; set; }

        public string XmlValue { get; set; }
    }
}
