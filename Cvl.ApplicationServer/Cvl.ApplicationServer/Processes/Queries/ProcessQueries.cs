using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Processes.Commands;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes.Queries
{
    public class ProcessQueries
    {
        private readonly ProcessInstanceContainerQueries _processInstanceContainerQueries;
        private readonly IServiceProvider _serviceProvider;

        public ProcessQueries(ProcessInstanceContainerQueries processInstanceContainerQueries,
            IServiceProvider serviceProvider)
        {
            _processInstanceContainerQueries = processInstanceContainerQueries;
            _serviceProvider = serviceProvider;
        }

        public async Task<TProcesInterface> LoadProcessAsync<TProcesInterface>(string processNumber, ClientConnectionData clientConnectionData)
            where TProcesInterface : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(TProcesInterface)) as TProcesInterface;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(TProcesInterface)}'");
            }
            
            var processInstanceContainer = await _processInstanceContainerQueries
                .GetProcessInstanceContainerByProcessNumber(processNumber);


            return process;

            //var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData,
            //    _fullSerializer, _jsonSerializer, _processInstanceContainerService, _processStateDataService
            //);

            ////_requestLoggerScope.ProcessId = process.ProcessId;
            //var generator = new ProxyGenerator();
            //var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            //return proxy;
        }
    }
}
