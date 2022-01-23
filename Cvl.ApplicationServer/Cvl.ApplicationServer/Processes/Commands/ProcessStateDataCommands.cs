using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes.Commands
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
