using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Core.Commands
{
    internal class ProcessStateDataCommands
    {
        private readonly ProcessStateDataRepository _processInstanceContainerRepository;

        public ProcessStateDataCommands(ProcessStateDataRepository processInstanceContainerRepository)
        {
            _processInstanceContainerRepository = processInstanceContainerRepository;
        }

        internal async Task UpdateAsync(ProcessStateData processInstanceStateData)
        {
            _processInstanceContainerRepository.Update(processInstanceStateData);
            await _processInstanceContainerRepository.SaveChangesAsync();
        }
    }
}
