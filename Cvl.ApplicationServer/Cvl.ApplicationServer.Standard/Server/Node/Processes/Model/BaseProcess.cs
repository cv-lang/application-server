using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cvl.ApplicationServer.Base.Model;
using Cvl.ApplicationServer.Monitoring.Base;
using Cvl.ApplicationServer.Monitoring.Base.Model;
using Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess;
using Cvl.ApplicationServer.UI.Attributes;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Proces bazowy dla innych procesów
    /// udostępnia szereg przydatnych funkcjonalności
    /// </summary>
    public class BaseProcess : BaseObject
    {
        #region Properties

        private const string gm = "Proces bazowy";

        [DataForm(GroupName = gm, Description = "Id procesu rodzica - jesli proces jest potmkiem")]
        public long? ParentId { get; set; }

        [DataForm(GroupName = gm, Description = "Parametr procesu przekazywany z zewnątrz - np. przez rodzica lub hosta")]
        public object ProcessParameter { get; set; }

        [Required(ErrorMessage = "Poprawny Email jest wymagany")]
        [DataForm(GroupName = gm, Description = "Status procesu")]
        public EnumProcessStatus ProcessStatus { get; set; }

        [DataForm(GroupName = gm, Description = "Data kiedy proces ma zostać wykonany - przed tą datą oczekuje na wykonanie")]
        public DateTime ExecutionDate { get; set; } = DateTime.Now;

        [DataForm(GroupName = gm, Description = "Dane związane z wyświetlaniem i pobieraniem danych do/od użytkownika")]
        public ProcessUI ProcessUI { get; set; } = new ProcessUI();

        [DataForm(GroupName = gm, Description = "Dane statystyczne i indeksowe procesu")]
        public ProcessData ProcessData { get; set; } = new ProcessData();       

        #endregion

        #region Base process method

        [Interpret]
        public object StartProcess(object inputParameter)
        {
            VirtualMachine.VirtualMachine.Hibernate();
            var ret = Start(inputParameter);
            ProcessStatus = EnumProcessStatus.Executed;
            return ret;
        }

        [Interpret]
        protected virtual object Start(object inputParameter)
        {
            return null;
        }

        [Interpret]
        protected void EndProcess(string formName, BaseModel model)
        {
            var formModel = new FormModel();
            formModel.ProcessId = Id;

            if (string.IsNullOrEmpty(formName) == false)
            {
                ProcessUI.FormDataToShow = new FormData(formName, formModel);
            }
            else
            {
                ProcessUI.FormDataToShow = null;
            }

            ProcessStatus = EnumProcessStatus.Executed;
            VirtualMachine.VirtualMachine.Hibernate();
        }

        protected void EndProcess(string header, string content)
        {
            EndProcess("GeneralView", new GeneralViewModel(header, content) { AutoRefresh = false });
        }


        #endregion        

        #region ShowForm               

        [Interpret]
        protected T ShowForm<T>(string formName, T model)
        where T : new()
        {
            var formModel = new FormModel<T>();
            formModel.ProcessId = Id;
            formModel.Layout = ProcessUI.ViewLayout;
            formModel.SetModel(model);

            ProcessUI.FormDataToShow = new FormData(ProcessUI.BaseViewPath +formName, formModel);
            
            //Log($"Wyświetlam: {ProcessUI.FormDataToShow.FormName}")
              //  .AddParameter(formModel, "formModel");

            
            ProcessLog.AddShowForm(ProcessUI.FormDataToShow, ShownFormType.ShownToUser);

            ProcessStatus = EnumProcessStatus.WaitingForUserData;
            VirtualMachine.VirtualMachine.Hibernate();

            ProcessLog.AddShowForm(ProcessUI.FormDataFromUser, ShownFormType.FromUser);

            return (T)ProcessUI.FormDataFromUser.FormModel.GetModel();
        }

        #endregion

        #region Steps data

        [DataForm(GroupName = gm, Description = "Krok w którym znajduje się proces")]
        public string ProcessStep { get; set; }
        [DataForm(GroupName = gm, Description = "Opis kroku w którym znajduje się proces")]
        public string ProcessStepDescription { get; set; }

        protected void SetStepData(string step, string stepDescription = null)
        {
            ProcessStep = step;
            ProcessStepDescription = stepDescription;
            ProcessLog.AddStep(step, stepDescription);
        }

        #endregion

        #region Child processes
        [DataForm(GroupName = gm, Description = "Lista identyfikatorów procesów potomnych")]
        public HashSet<long> ChildProcesses { get; set; } = new HashSet<long>();

        [Interpret]
        protected long CreateNewChildProcess<T>(object childProcessParameter = null)
            where T : BaseProcess, new()
        {
            var childProcess = new T();
            childProcess.ParentId = Id;
            childProcess.ProcessParameter = childProcessParameter;

            ProcessStatus = EnumProcessStatus.WaitingForHost;
            var ret = VirtualMachine.VirtualMachine.Hibernate(EnumHostCommand.CreateChildProcess, childProcess);
            childProcess.Id = (long)ret;

            ProcessLog.AddChildProcess(childProcess);
            return childProcess.Id;
        }

        [Interpret]
        protected void WaitToEndAllChildProcesses()
        {
            ProcessStatus = EnumProcessStatus.WaitingForHost;
            VirtualMachine.VirtualMachine.Hibernate(EnumHostCommand.WaitForAllChildrenEnd);
        }

        #endregion

        #region External communication

        protected void AddExternalCommunication(string type, string header, string content, params object[] parameters)
        {
            ProcessLog.AddExternalCommunication(type, header, content, parameters);
        }

        #endregion

        #region Logs

        public ProcessLog ProcessLog { get; set; } = new ProcessLog();

        protected LogModel Log(string log)
        {
            return ProcessLog.Logger.Info(log);
        }
        #endregion              
        
    }
}
