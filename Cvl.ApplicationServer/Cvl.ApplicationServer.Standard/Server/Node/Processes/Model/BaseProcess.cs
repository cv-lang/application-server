﻿using System;
using System.Collections.Generic;
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
        public long ParentId { get; set; }
        public ProcessId GetProcessIdentificator() => new ProcessId(Id);
        public EnumProcessStatus ProcessStatus { get; set; }
        public string BaseViewPath { get; set; }


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

        public FormData FormDataToShow { get; set; }
        public FormData FormDataFromUser { get; set; }

        [Interpret]
        protected T ShowForm<T>(string formName, T formModel)
        where T : BaseModel
        {
            formModel.ProcessId = Id;
            FormDataToShow = new FormData(BaseViewPath+formName, formModel);

            ProcessStatus = EnumProcessStatus.WaitingForUserData;
            VirtualMachine.VirtualMachine.Hibernate();

            return formModel;
        }

        #endregion

        #region Child processes

        public HashSet<long> ChildProcesses { get; set; } = new HashSet<long>();
        
        protected void CreateNewChildProcess<T>()
            where T : BaseProcess, new()
        {
            var childProcess = new T();
            childProcess.ParentId = Id;
            ProcessStatus = EnumProcessStatus.WaitingForHost;
            VirtualMachine.VirtualMachine.Hibernate(childProcess);
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
