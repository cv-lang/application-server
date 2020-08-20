namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Proces bazowy dla innych procesów
    /// udostępnia szereg przydatnych funkcjonalności
    /// </summary>
    public class BaseProcess
    {
        public int ProcessIdentificator { get; set; }
        public ProcessId GetId() => new ProcessId(ProcessIdentificator);
        public EnumProcessStatus ProcessStatus { get; set; }
    }
}
