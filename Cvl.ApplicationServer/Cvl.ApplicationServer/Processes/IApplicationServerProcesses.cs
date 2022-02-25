using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Dtos;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.UI;

namespace Cvl.ApplicationServer.Processes
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
