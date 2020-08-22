using System;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;
using Cvl.VirtualMachine.Core.Attributes;

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

        

        [Interpret]
        public object StartProcess(object inputParameter)
        {
            VirtualMachine.VirtualMachine.Hibernate();
            return Start(inputParameter);
        }

        [Interpret]
        protected virtual object Start(object inputParameter)
        {
            return null;
        }


        #region ShowForm

        public FormData FormDataToShow { get; set; }
        public FormData FormDataFromUser { get; set; }

        [Interpret]
        protected T ShowForm<T>(string formName, T formModel)
        {
            FormDataToShow = new FormData(formName, formModel);

            ProcessStatus = EnumProcessStatus.WaitingForUserData;
            VirtualMachine.VirtualMachine.Hibernate();

            return formModel;
        }

        #endregion


        protected void ThrowError(string throwError, object parameter = null)
        {
            throw new NotImplementedException();
        }

        protected void Log(string log)
        {

        }
    }
}
