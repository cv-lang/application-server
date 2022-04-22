using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Repositories;
using Microsoft.EntityFrameworkCore;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.Processes.Queries
{
    internal class ProcessInstanceContainerQueries
    {
        private readonly ProcessInstanceContainerRepository _processInstanceContainerRepository;

        public ProcessInstanceContainerQueries(ProcessInstanceContainerRepository processInstanceContainerRepository)
        {
            _processInstanceContainerRepository = processInstanceContainerRepository;
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerWithNestedObject(long processId)
        {
            return await _processInstanceContainerRepository.GetAll()
                .Include(x => x.ProcessDiagnosticData)
                .Include(x => x.ProcessInstanceStateData)
                .Include(x => x.ProcessExternalData)
                .SingleAsync(x => x.Id == processId);
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerWithNestedObjectByProcessNumber(string processNumber)
        {
            return await _processInstanceContainerRepository.GetAll()
                .Include(x => x.ProcessDiagnosticData)
                .Include(x => x.ProcessInstanceStateData)
                .Include(x => x.ProcessExternalData)
                .SingleAsync(x => x.ProcessNumber == processNumber);
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerByProcessNumberAsync(string processNumber)
        {
            return await _processInstanceContainerRepository.GetAll()
                .SingleAsync(x => x.ProcessNumber == processNumber);
        }
        

        internal async Task<List<string>> GetWaitingForExecutionProcessesNumbersAsync(ProcessType processType)
        {
            var now = DateTime.UtcNow;
            return await _processInstanceContainerRepository.GetAll()
                .Where(x=> x.ProcessTypeData.ProcessType == processType)
                .Where(x => x.ThreadData.MainThreadState == ThreadState.WaitingForExecution)
                .Where(x => x.ThreadData.NextExecutionDate == null || (x.ThreadData.NextExecutionDate < now))
                .Select(x => x.ProcessNumber)
                .ToListAsync();
        }

        public IQueryable<ProcessInstanceContainer> GetAll()
        {
            return _processInstanceContainerRepository.GetAll();
        }

        public IQueryable<ProcessInstanceContainer> GetAllWithDiagnosticeAndExternal()
        {
            return _processInstanceContainerRepository.GetAll()
                    .Include(x => x.ProcessDiagnosticData)
                    .Include(x => x.ProcessExternalData)
                ;
        }
    }
}
