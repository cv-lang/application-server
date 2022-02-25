using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Commands
{
    public class ProcessStateDataCommands
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
