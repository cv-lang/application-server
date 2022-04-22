namespace Cvl.ApplicationServer.Processes.Core.Base
{
    public abstract class BaseProcess : IProcess, IStateProcess
    {        
        public ProcessData? ProcessData { get; set; }

        public abstract void LoadProcessState(object? processState);

        public abstract object? GetProcessState();

        public virtual void JobEntry()
        {
        }
    }
}
