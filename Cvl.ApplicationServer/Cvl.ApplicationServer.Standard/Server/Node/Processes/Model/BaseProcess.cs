using System;
using System.Collections.Generic;
using Cvl.ApplicationServer.Base.Model;
using Cvl.ApplicationServer.Monitoring.Base;
using Cvl.ApplicationServer.Monitoring.Base.Model;
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
        private const string gm = "Proces bazowy";

        [DataForm(GroupName = gm, Description = "Id procesu rodzica - jesli proces jest potmkiem")]
        public long? ParentId { get; set; }

        public ProcessId GetProcessIdentificator() => new ProcessId(Id);

        [DataForm(GroupName = gm, Description = "Status procesu")]
        public EnumProcessStatus ProcessStatus { get; set; }

        [DataForm(GroupName = gm, Description = "Ścieżka do folderu widoków - dodawana jest do nazwy widoku")]
        public string BaseViewPath { get; set; }

        [DataForm(GroupName = gm, Description = "Nazwa layoutu widoku")]
        public string ViewLayout { get; set; } = "_Layout";

        #region Steps data

        [DataForm(GroupName = gm, Description = "Krok w którym znajduje się proces")]
        public string ProcessStep { get; set; }
        [DataForm(GroupName = gm, Description = "Opis kroku w którym znajduje się proces")]
        public string ProcessStepDescription { get; set; }

        protected void SetStepData(string step, string stepDescription=null)
        {
            ProcessStep = step;
            ProcessStepDescription = stepDescription;
        }

        #endregion

        #region External ID

        [DataForm(GroupName = gm, Description = "Dane instancji procesu, różne per instancja procesu(proces)")]
        public string ProcessInstanceDescription { get; set; }

        [DataForm(GroupName = gm, Description = "Dane instancji procesu, różne per instancja procesu(proces)")]
        public string ProcessInstanceFullDescription { get; set; }

        [DataForm(GroupName = gm, Description = "Parametr procesu (np. external id, email, NIP, PESEL...)")]
        public string ProcessParameter { get; set; }

        [DataForm(GroupName = gm, Description = "Parametr2 procesu (np. external id, NIP, PESEL...)")]
        public string ProcessParameter2 { get; set; }

        #endregion

        [Interpret]
        public object StartProcess(object inputParameter)
        {
            VirtualMachine.VirtualMachine.Hibernate();
            var ret= Start(inputParameter);
            ProcessStatus = EnumProcessStatus.Executed;
            return ret;
        }

        [Interpret]
        protected virtual object Start(object inputParameter)
        {
            return null;
        }


        #region ShowForm

        [DataForm(GroupName = gm, Description = "Dane które są wyświetlane użytkownikowi")]
        public FormData FormDataToShow { get; set; }

        [DataForm(GroupName = gm, Description = "Pobrane dane od użytkownika")]
        public FormData FormDataFromUser { get; set; }

        [Interpret]
        protected T ShowForm<T>(string formName, T formModel)
        where T : BaseModel
        {
            formModel.ProcessId = Id;
            formModel.Layout = ViewLayout;
            FormDataToShow = new FormData(BaseViewPath+formName, formModel);

            Log($"Wyświetlam: {FormDataToShow.FormName}")
                .AddParameter(formModel, "formModel");

            ProcessStatus = EnumProcessStatus.WaitingForUserData;
            VirtualMachine.VirtualMachine.Hibernate();

            return (T)FormDataFromUser.FormDataModel;
        }

        #endregion

        #region Child processes
        [DataForm(GroupName = gm, Description = "Lista identyfikatorów procesów potomnych")]
        public HashSet<long> ChildProcesses { get; set; } = new HashSet<long>();

        [Interpret]
        protected long CreateNewChildProcess<T>()
            where T : BaseProcess, new()
        {
            var childProcess = new T();
            childProcess.ParentId = Id;
            ProcessStatus = EnumProcessStatus.WaitingForHost;
            var ret = VirtualMachine.VirtualMachine.Hibernate(EnumHostCommand.CreateChildProcess, childProcess);
            return (long)ret;
        }

        [Interpret]
        protected void WaitToEndAllChildProcesses()
        {
            ProcessStatus = EnumProcessStatus.WaitingForHost;
            VirtualMachine.VirtualMachine.Hibernate(EnumHostCommand.WaitForAllChildrenEnd);
        }

        #endregion

        #region Logs

        public Logger Logger { get; set; } = new Logger();

        #endregion

        [Interpret]
        protected void EndProcess(string formName, BaseModel formModel)
        {
            formModel.ProcessId = Id;

            if (string.IsNullOrEmpty(formName) == false)
            {
                FormDataToShow = new FormData(formName, formModel);
            }
            else
            {
                FormDataToShow = null;
            }

            ProcessStatus = EnumProcessStatus.Executed;
            VirtualMachine.VirtualMachine.Hibernate();
        }

        protected void EndProcess(string header, string content)
        {
            EndProcess("GeneralView", new GeneralViewModel(header, content){AutoRefresh = false});
        }

        protected LogModel Log(string log)
        {
            return Logger.Info(log);
        }
    }
}
