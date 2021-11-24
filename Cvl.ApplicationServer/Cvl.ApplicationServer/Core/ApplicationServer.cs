﻿using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core
{
    public class ApplicationServer
    {
        private readonly IFullSerializer _fullSerializer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ProcessInstanceService _processInstanceService;
        private readonly ProcessService _processService;

        public ApplicationServer(IFullSerializer fullSerializer, IJsonSerializer jsonSerializer,
            ProcessInstanceService processInstanceService,
            ProcessService processService)
        {
            this._processInstanceService = processInstanceService;
            this._processService = processService;
            this._fullSerializer = fullSerializer;
            this._jsonSerializer = jsonSerializer;
        }

        public IQueryable<ProcessListItemDto> GetAllProcesses()
        {
            return _processService.GetAllProcesses();
        }

        public TProcesInterface CreateProcess<TProcesInterface>(Model.ClientConnectionData clientConnectionData) 
            where TProcesInterface : class, IProcess            
        {
            var process = _processInstanceService.CreateProcess<TProcesInterface>();
            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData,_fullSerializer, _jsonSerializer, _processInstanceService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }

        internal TProcesInterface LoadProcess<TProcesInterface>(long processId, ClientConnectionData clientConnectionData)
             where TProcesInterface : class, IProcess
        {
            var process = _processInstanceService.LoadProcess<TProcesInterface>(processId);

            var processProxy = new ProcessInterceptorProxy<TProcesInterface>(process, clientConnectionData, _fullSerializer, _jsonSerializer, _processInstanceService);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithTarget<TProcesInterface>(process, processProxy);
            return proxy;
        }
    }
}
