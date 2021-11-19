using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProcessInstanceRepository _repository;

        public ProcessService(IServiceProvider serviceProvider, ProcessInstanceRepository repository)
        {
           _serviceProvider = serviceProvider;
            this._repository = repository;
        }

        internal T CreateProcess<T>() where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;

            var processInstance = new ProcessInstance();
            _repository.Insert(processInstance);
            process.ProcessId = processInstance.Id;

            return process;
        }
    }
}
