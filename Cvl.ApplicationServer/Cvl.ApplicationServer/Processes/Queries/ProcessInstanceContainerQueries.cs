using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Queries
{
    public class ProcessInstanceContainerQueries
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
                .SingleAsync(x => x.Id == processId);
        }

        public async Task<ProcessInstanceContainer> GetProcessInstanceContainerByProcessNumber(string processNumber)
        {
            return await _processInstanceContainerRepository.GetAll()
                .Include(x => x.ProcessDiagnosticData)
                .Include(x => x.ProcessInstanceStateData)
                .SingleAsync(x => x.ProcessNumber == processNumber);
        }
    }
}
