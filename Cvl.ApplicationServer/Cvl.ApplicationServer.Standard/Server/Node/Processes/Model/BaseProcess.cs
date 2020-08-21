using System;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Proces bazowy dla innych procesów
    /// udostępnia szereg przydatnych funkcjonalności
    /// </summary>
    public class BaseProcess
    {
        public long Id { get; set; }
        public ProcessId GetProcessIdentificator() => new ProcessId(Id);
        public EnumProcessStatus ProcessStatus { get; set; }

        public void StartProcess(object inputParameter)
        {
            VirtualMachine.VirtualMachine.Hibernate();
            Start(inputParameter);
        }
        public virtual object Start(object inputParameter)
        {
            return null;
        }

        protected T ShowForm<T>(string formName,T formModel)
        {
            return formModel;
        }

        protected void ThrowError(string throwError, object parameter = null)
        {
            throw new NotImplementedException();
        }

        protected void Log(string log)
        {

        }
    }
}
