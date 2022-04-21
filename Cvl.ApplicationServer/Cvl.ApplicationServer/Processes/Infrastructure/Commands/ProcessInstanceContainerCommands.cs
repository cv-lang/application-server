using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.Repositories;
using Cvl.ApplicationServer.Core.Processes.Services;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Commands
{
    internal class ProcessInstanceContainerCommands
    {
        private readonly ProcessInstanceContainerRepository _processInstanceContainerRepository;
        private readonly IProcessNumberGenerator _processNumberGenerator;

        public ProcessInstanceContainerCommands(ProcessInstanceContainerRepository processInstanceContainerRepository,
            IProcessNumberGenerator processNumberGenerator)
        {
            _processInstanceContainerRepository = processInstanceContainerRepository;
            _processNumberGenerator = processNumberGenerator;
        }

        public async Task<ProcessInstanceContainer> CreateProcessInstanceContainer(Type processType, string processState)
        {
            var processInstanceContainer = new ProcessInstanceContainer("",
                "new");

            processInstanceContainer.ProcessTypeData.ProcessTypeFullName = processType.AssemblyQualifiedName!;
            processInstanceContainer.ProcessInstanceStateData = new ProcessStateData(processState);
            processInstanceContainer.ProcessDiagnosticData = new ProcessDiagnosticData();
            processInstanceContainer.ProcessExternalData = new ProcessExternalData();
            

            _processInstanceContainerRepository.Insert(processInstanceContainer);
            await _processInstanceContainerRepository.SaveChangesAsync();
            
            processInstanceContainer.ProcessNumber = _processNumberGenerator.GenerateProcessNumber(processInstanceContainer.Id);
            _processInstanceContainerRepository.Update(processInstanceContainer);
            await _processInstanceContainerRepository.SaveChangesAsync();

            return processInstanceContainer;
        }

        internal async Task SetExternalDataAsync(string processNumber, string xml)
        {
            var processContainer = await _processInstanceContainerRepository.GetAll()
                .Include(x=> x.ProcessExternalData)
                .SingleAsync(x=> x.ProcessNumber == processNumber);

            processContainer.ProcessExternalData.ProcessExternalDataFullSerialization = xml;
            processContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExecution;
            await _processInstanceContainerRepository.SaveChangesAsync();
        }
    }
}
