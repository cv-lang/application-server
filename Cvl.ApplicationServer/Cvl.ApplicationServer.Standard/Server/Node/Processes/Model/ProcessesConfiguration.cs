using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Konfiguracja procesów - do wczytania i uruchomienia
    /// </summary>
    public class ProcessesConfiguration
    {
        public List<ProcessTypeDescription> Processes { get; set; } = new List<ProcessTypeDescription>();
    }
}
