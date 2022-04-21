using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Commands
{
    internal class ProcessExternalDataCommands
    {
        private readonly ProcessExternalDataRepository _processExternalDataRepository;

        public ProcessExternalDataCommands(ProcessExternalDataRepository processExternalDataRepository)
        {
            _processExternalDataRepository = processExternalDataRepository;
        }

        internal async Task UpdateAsync(ProcessExternalData processExternalData)
        {
            _processExternalDataRepository.Update(processExternalData);
            await _processExternalDataRepository.SaveChangesAsync();
        }
    }
}
