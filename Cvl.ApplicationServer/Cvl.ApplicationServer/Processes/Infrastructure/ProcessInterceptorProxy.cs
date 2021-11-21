using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Infrastructure
{
    public class ProcessInterceptorProxy<T> : IInterceptor
        where T : IProcess
    {
        private IProcess _process;
        private readonly ClientConnectionData _clientConnectionData;
        private readonly IFullSerializer _serializer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ProcessService _processService;

        public ProcessInterceptorProxy(IProcess process, ClientConnectionData clientConnectionData, 
            IFullSerializer serializer, IJsonSerializer jsonSerializer, ProcessService processService)
        {
            _process = process;
            _clientConnectionData = clientConnectionData;
            _serializer = serializer;
            _jsonSerializer = jsonSerializer;
            _processService = processService;
        }

        public void Intercept(IInvocation invocation)
        {
            var request = _serializer.Serialize(invocation.Arguments.ToList());
            var jsonRequest = _jsonSerializer.Serialize(invocation.Arguments.ToList());

            //zapisuje dane requestu
            var activityData = new ProcessActivityData(request, jsonRequest, "requestType");
            _processService.Insert(activityData);

            var activity = new ProcessActivity(_process.ProcessId,
                _clientConnectionData.ClientIpAddress, _clientConnectionData.ClientIpPort, _clientConnectionData.GetClientConnectionData(),
                invocation.Method.Name,
                DateTime.Now, request?.Truncate(140) ?? "", null, null, activityData.Id
                );

            _processService.Insert(activity);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                var errorRespnse = ex.ToString();
                activityData.ResponseFullSerialization = errorRespnse;
                _processService.Update(activityData);

                throw;
            }

            var response = _serializer.Serialize(invocation.ReturnValue);
            var jsonResponse= _jsonSerializer.Serialize(invocation.ReturnValue);
            activityData.ResponseFullSerialization = response;
            activityData.ResponseJson = jsonResponse;
            _processService.Update(activityData);

            activity.ResponseDate = DateTime.Now;
            activity.PreviewResponseJson = jsonResponse.Truncate(140);

            invocation.ReturnValue = invocation.ReturnValue;
        }
    }
}
