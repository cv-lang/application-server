using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Commands
{
    public class ProcessInstanceContainerCommands
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

            processInstanceContainer.ProcessTypeData.ProcessTypeFullName = processType.FullName!;
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
    }
}
