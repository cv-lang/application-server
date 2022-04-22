using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Repositories;
using Cvl.ApplicationServer.Processes.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Commands
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
