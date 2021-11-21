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
            var requestFulSerializeString = "";
            var requestJsonSerializeStrong = "";
            var requestType = "";

            if(invocation.Arguments.Count() == 1)
            {
                var request = invocation.Arguments[0];
                requestType = request.GetType().FullName;
                requestFulSerializeString= _serializer.Serialize(request);
                requestJsonSerializeStrong = _jsonSerializer.Serialize(request);
            }
            else
            {
                requestFulSerializeString = _serializer.Serialize(invocation.Arguments.ToList());
                requestJsonSerializeStrong = _jsonSerializer.Serialize(invocation.Arguments.ToList());
                requestType = invocation.Arguments.ToList().GetType().FullName;
            }
                      

            //zapisuje dane requestu
            var activityData = new ProcessActivityData(requestFulSerializeString, requestJsonSerializeStrong, requestType);
            _processService.Insert(activityData);

            var activity = new ProcessActivity(_process.ProcessId,
                _clientConnectionData.ClientIpAddress, _clientConnectionData.ClientIpPort, _clientConnectionData.GetClientConnectionData(),
                ProcessActivityState.Executing, invocation.Method.Name,
                DateTime.Now, requestFulSerializeString?.Truncate(140) ?? "", null, null, activityData.Id
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
                activity.PreviewResponseJson = $"Error: {ex.Message}";
                activity.ActivityState = ProcessActivityState.Exception;
                _processService.Update(activityData);

                throw;
            }

            var response = _serializer.Serialize(invocation.ReturnValue);
            var jsonResponse= _jsonSerializer.Serialize(invocation.ReturnValue);
            activityData.ResponseFullSerialization = response;
            activityData.ResponseJson = jsonResponse;
            activityData.ResponseType = invocation.ReturnValue?.GetType()?.FullName;
            _processService.Update(activityData);                      

            activity.ResponseDate = DateTime.Now;
            activity.PreviewResponseJson = jsonResponse.Truncate(140);
            activity.ActivityState = ProcessActivityState.Executed;

            _processService.SerializeProcess(_process);

            invocation.ReturnValue = invocation.ReturnValue;
        }
    }
}
