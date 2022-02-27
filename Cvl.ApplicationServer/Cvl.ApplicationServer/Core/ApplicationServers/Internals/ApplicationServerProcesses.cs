using Cvl.ApplicationServer.Core.Processes;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Dtos;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Workers;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;
using Microsoft.EntityFrameworkCore;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.ApplicationServers.Internals
{
    internal class ApplicationServerProcesses : IApplicationServerProcesses
    {
        private readonly IProcessesWorker _worker;
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;
        private readonly ProcessExternalDataCommands _processExternalDataCommands;
        private readonly ProcessActivityQueries _processActivityQueries;
        private readonly ProcessStepQueries _processStepQueries;

        public ApplicationServerProcesses(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer,
            ProcessExternalDataCommands processExternalDataCommands,
            ProcessActivityQueries processActivityQueries,
            ProcessStepQueries processStepQueries,
            IProcessesWorker worker)
        {
            _worker = worker;
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
            _processExternalDataCommands = processExternalDataCommands;
            _processActivityQueries = processActivityQueries;
            _processStepQueries = processStepQueries;
        }

        #region Simple process

        public T CreateProcess<T>() where T : IProcess
        {
            return _processCommands.CreateProcessAsync<T>().Result;
        }

        public IProcess LoadProcess(string processNumber)
        {
            return _processQueries.LoadProcessAsync<IProcess>(processNumber).Result;
        }

        public T LoadProcess<T>(string processNumber) where T : IProcess
        {
            return (T)LoadProcess(processNumber);
        }

        #endregion


        public T StartProcess<T>() where T : IProcess
        {
            var process = CreateProcess<T>();

            return process;
        }


        


        public void SaveProcess(IProcess process)
        {
            _processCommands.SaveProcessStateAsync(process).Wait();
        }

        

        

        public IQueryable<ProcessInstanceContainer> GetAllProcesses()
        {
            return _processQueries.GetAllProcessInstanceContainers();
        }

        public IQueryable<ProcessListItemDto> GetAllProcessesDto()
        {
            var list = _processQueries.GetAllProcessInstanceContainers();

            var listDto = list.Include(x => x.ProcessDiagnosticData)
                .Select(x => new ProcessListItemDto(x));
            return listDto;
        }

        public IQueryable<ProcessActivity> GetProcessActivities(long processId)
        {
            return _processActivityQueries.GetAll();
        }

        public IQueryable<ProcessStepHistory> GetProcessSteps(long processId)
        {
            return _processStepQueries.GetAll();
        }

        public T OpenProcessProxy<T>(string processNumber) where T : IProcess, IDisposable
        {
            throw new NotImplementedException();
        }

        public ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess
        {
            return _worker.StartLongRunningProcess<T>(inputParameter);
        }
    }
}
