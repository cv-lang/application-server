using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Processes.Model;
using Cvl.ApplicationServer.Processes.Repositories;

namespace Cvl.ApplicationServer.Processes.Commands
{
    public class ProcessExternalDataCommands
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
