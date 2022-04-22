using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Core.Commands
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
