using Cvl.ApplicationServer.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.ProcessEngine.Model
{
    /// <summary>
    /// Proces bazowy dla innych procesów
    /// udostępnia szereg przydatnych funkcjonalności
    /// </summary>
    public class BaseProcess
    {
        public int ProcessIdentificator { get; set; }
        public ProcessId GetId()=> new ProcessId(ProcessIdentificator);
        public EnumProcessStatus ProcessStatus { get; set; }
    }
}
