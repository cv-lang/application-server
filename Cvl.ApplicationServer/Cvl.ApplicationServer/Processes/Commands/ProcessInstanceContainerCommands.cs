using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Processes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Commands
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

        public async Task<ProcessInstanceContainer> CreateProcessInstanceContainer(Type processType)
        {
            var processInstanceContainer = new ProcessInstanceContainer("", processType.FullName!,
                "new");

            processInstanceContainer.ProcessInstanceStateData = new ProcessStateData(string.Empty);
            processInstanceContainer.ProcessDiagnosticData = new ProcessDiagnosticData();

            _processInstanceContainerRepository.Insert(processInstanceContainer);
            await _processInstanceContainerRepository.SaveChangesAsync();
            
            processInstanceContainer.ProcessNumber = _processNumberGenerator.GenerateProcessNumber(processInstanceContainer.Id);
            _processInstanceContainerRepository.Update(processInstanceContainer);
            await _processInstanceContainerRepository.SaveChangesAsync();

            return processInstanceContainer;
        }
    }
}
