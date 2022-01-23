using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Old.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Commands;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes.Queries
{
    public class ProcessQueries
    {
        private readonly ProcessInstanceContainerQueries _processInstanceContainerQueries;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFullSerializer _fullSerializer;

        public ProcessQueries(ProcessInstanceContainerQueries processInstanceContainerQueries,
            IServiceProvider serviceProvider, IFullSerializer fullSerializer)
        {
            _processInstanceContainerQueries = processInstanceContainerQueries;
            _serviceProvider = serviceProvider;
            _fullSerializer = fullSerializer;
        }

        public async Task<TProcesInterface> LoadProcessAsync<TProcesInterface>(string processNumber)
            where TProcesInterface : IProcess
        {
            var processInstanceContainer = await _processInstanceContainerQueries
                .GetProcessInstanceContainerByProcessNumber(processNumber);

            var processType = Type.GetType(processInstanceContainer.Type);

            var process = (TProcesInterface)_serviceProvider.GetService(processType) ;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(TProcesInterface)}'");
            }

            process.ProcessData = new ProcessData();
            process.ProcessData.ProcessInstanceContainer = processInstanceContainer;

            var state = _fullSerializer.Deserialize<object>(processInstanceContainer.ProcessInstanceStateData.ProcessStateFullSerialization);
            process.LoadProcessState(state);

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
