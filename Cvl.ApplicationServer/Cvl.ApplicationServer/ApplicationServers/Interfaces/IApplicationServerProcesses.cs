using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;
using Cvl.ApplicationServer.Processes.UI;
using Cvl.ApplicationServer.Test;

namespace Cvl.ApplicationServer.Core.Interfaces
{
    public interface IApplicationServerProcesses
    {
        T CreateProcess<T>() where T : IProcess;
        void SaveProcess(IProcess process);
        IProcess LoadProcess(string processNumber);
        T LoadProcess<T>(string processNumber) where T : IProcess;
        ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess;
        int RunProcesses();
        void SetExternalDataInput(string processNumber, object externalData = null);
        object GetExternalDataOutput(string processNumber);
        object GetExternalDataInput(string processNumber);
        View GetViewData(string processNumber);
        void SetViewResponse(string processNumbr, ViewResponse viewResponse);
        IQueryable<ProcessInstanceContainer> GetAllProcesses();
        IQueryable<ProcessListItemDto> GetAllProcessesDto();
        IQueryable<ProcessActivity> GetProcessActivities(long processId);
        IQueryable<ProcessStepHistory> GetProcessSteps(long processId);
        public T StartProcess<T>() where T : IProcess;
    }
}
