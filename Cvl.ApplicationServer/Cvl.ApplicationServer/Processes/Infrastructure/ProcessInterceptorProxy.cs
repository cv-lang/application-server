using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Extensions;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Infrastructure
{



    public class ProcessInterceptorProxy<T> : AsyncInterceptorBase
        where T : IProcess
    {
        private IProcess _process;
        private readonly ClientConnectionData _clientConnectionData;
        private readonly IFullSerializer _serializer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ProcessInstanceContainerService _processService;

        public ProcessInterceptorProxy(IProcess process, ClientConnectionData clientConnectionData,
            IFullSerializer serializer, IJsonSerializer jsonSerializer, ProcessInstanceContainerService processService)
        {
            _process = process;
            _clientConnectionData = clientConnectionData;
            _serializer = serializer;
            _jsonSerializer = jsonSerializer;
            _processService = processService;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            var activity = await pre(invocation);
            try
            {                
                // Cannot simply return the the task, as any exceptions would not be caught below.
                await proceed(invocation, proceedInfo).ConfigureAwait(false);

                await post(invocation, null, activity);
            }
            catch (Exception ex)
            {
                await execption(ex, activity);
                //Log.Error($"Error calling {invocation.Method.Name}.", ex);
                throw;
            }
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var activity = await pre(invocation);
            try
            {                
                // Cannot simply return the the task, as any exceptions would not be caught below.
                var response =  await proceed(invocation, proceedInfo).ConfigureAwait(false);

                await post(invocation, response, activity);

                return response;
            }
            catch (Exception ex)
            {
                await execption(ex, activity);
                //Log.Error($"Error calling {invocation.Method.Name}.", ex);
                throw;
            }
        }

        private async Task<Tuple<ProcessActivity, ProcessActivityData>> pre(IInvocation invocation)
        {
            var requestFulSerializeString = "";
            var requestJsonSerializeStrong = "";
            string requestType = "";

            if (invocation.Arguments.Count() == 1)
            {
                var request = invocation.Arguments[0];
                requestType = request.GetType().FullName!;
                requestFulSerializeString = _serializer.Serialize(request);
                requestJsonSerializeStrong = _jsonSerializer.Serialize(request);
            }
            else
            {
                requestFulSerializeString = _serializer.Serialize(invocation.Arguments.ToList());
                requestJsonSerializeStrong = _jsonSerializer.Serialize(invocation.Arguments.ToList());
                requestType = invocation.Arguments.ToList().GetType().FullName!;
            }


            //zapisuje dane requestu
            var activityData = new ProcessActivityData(requestFulSerializeString, requestJsonSerializeStrong, requestType);

            var activity = new ProcessActivity(_process.ProcessId,
                _clientConnectionData.ClientIpAddress, _clientConnectionData.ClientIpPort, _clientConnectionData.GetClientConnectionData(),
                ProcessActivityState.Executing, invocation.Method.Name,
                DateTime.Now, requestFulSerializeString?.Truncate(ProcessActivity.JsonPreviewSize) ?? "", null, null
                );
            activity.ProcessActivityData = activityData;

            await _processService.InsertProcessActivityAsync(activity);

            return new Tuple<ProcessActivity, ProcessActivityData>(activity, activityData);
        }

        private async Task post(IInvocation invocation, object returnValue, Tuple<ProcessActivity, ProcessActivityData> activity)
        {
            var response = _serializer.Serialize(returnValue);
            var jsonResponse = _jsonSerializer.Serialize(returnValue);

            await _processService.UpdateActivityResponseAsync(response, jsonResponse, returnValue?.GetType()?.FullName,
                activity.Item1, activity.Item2);


            await _processService.SerializeProcessAsync(_process);
        }

        private async Task execption(Exception ex, Tuple<ProcessActivity, ProcessActivityData> activity)
        {
            var errorRespnse = ex.ToString();
            await _processService.UpdateActivityErrorAsync(ex, activity.Item1, activity.Item2);
        }
    }


    //public class ProcessInterceptorProxy<T> : AsyncInterceptor
    //    where T : IProcess
    //{
    //    private IProcess _process;
    //    private readonly ClientConnectionData _clientConnectionData;
    //    private readonly IFullSerializer _serializer;
    //    private readonly IJsonSerializer _jsonSerializer;
    //    private readonly ProcessInstanceContainerService _processService;

    //    public ProcessInterceptorProxy(IProcess process, ClientConnectionData clientConnectionData, 
    //        IFullSerializer serializer, IJsonSerializer jsonSerializer, ProcessInstanceContainerService processService)
    //    {
    //        _process = process;
    //        _clientConnectionData = clientConnectionData;
    //        _serializer = serializer;
    //        _jsonSerializer = jsonSerializer;
    //        _processService = processService;
    //    }


    //    protected override async Task<Object> InterceptAsync(object target, MethodBase method, object[] arguments, Func<Task<object>> proceed)
    //    {            

    //        var requestFulSerializeString = "";
    //        var requestJsonSerializeStrong = "";
    //        string requestType = "";

    //        if(arguments.Count() == 1)
    //        {
    //            var request = arguments[0];
    //            requestType = request.GetType().FullName!;
    //            requestFulSerializeString= _serializer.Serialize(request);
    //            requestJsonSerializeStrong = _jsonSerializer.Serialize(request);
    //        }
    //        else
    //        {
    //            requestFulSerializeString = _serializer.Serialize(arguments.ToList());
    //            requestJsonSerializeStrong = _jsonSerializer.Serialize(arguments.ToList());
    //            requestType = arguments.ToList().GetType().FullName!;
    //        }


    //        //zapisuje dane requestu
    //        var activityData = new ProcessActivityData(requestFulSerializeString, requestJsonSerializeStrong, requestType);

    //        var activity = new ProcessActivity(_process.ProcessId,
    //            _clientConnectionData.ClientIpAddress, _clientConnectionData.ClientIpPort, _clientConnectionData.GetClientConnectionData(),
    //            ProcessActivityState.Executing, method.Name,
    //            DateTime.Now, requestFulSerializeString?.Truncate(ProcessActivity.JsonPreviewSize) ?? "", null, null
    //            );
    //        activity.ProcessActivityData = activityData;

    //        await _processService.InsertProcessActivityAsync(activity);

    //        object returnValue;
    //        try
    //        {
    //            returnValue = await proceed();

    //        }
    //        catch (Exception ex)
    //        {
    //            var errorRespnse = ex.ToString();     
    //            await _processService.UpdateActivityErrorAsync(ex, activity, activityData);

    //            throw;
    //        }


    //        var response = _serializer.Serialize(returnValue);
    //        var jsonResponse= _jsonSerializer.Serialize(returnValue);

    //        await _processService.UpdateActivityResponseAsync(response, jsonResponse, returnValue?.GetType()?.FullName,
    //            activity, activityData);


    //        await _processService.SerializeProcessAsync(_process);

    //        return returnValue;
    //    }
    //}




    //public abstract class AsyncInterceptor : IInterceptor
    //{
    //    class TaskCompletionSourceMethodMarkerAttribute : Attribute
    //    {

    //    }

    //    private static readonly MethodInfo _taskCompletionSourceMethod = typeof(AsyncInterceptor)
    //        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
    //        .Single(x => x.GetCustomAttributes(typeof(TaskCompletionSourceMethodMarkerAttribute)).Any());


    //    protected virtual Task<Object> InterceptAsync(Object target, MethodBase method, object[] arguments, Func<Task<Object>> proceed)
    //    {
    //        return proceed();
    //    }

    //    protected virtual void Intercept(Object target, MethodBase method, object[] arguments, Action proceed)
    //    {
    //        proceed();
    //    }

    //    [TaskCompletionSourceMethodMarker]
    //    Task<TResult> TaskCompletionSource<TResult>(IInvocation invocation)
    //    {
    //        var tcs = new TaskCompletionSource<TResult>();

    //        var task = InterceptAsync(invocation.InvocationTarget, invocation.Method, invocation.Arguments, () =>
    //        {
    //            var task2 = (Task)invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments);
    //            var tcs2 = new TaskCompletionSource<Object>();
    //            task2.ContinueWith(x =>
    //            {
    //                if (x.IsFaulted)
    //                {
    //                    tcs2.SetException(x.Exception);
    //                    return;
    //                }
    //                dynamic dynamicTask = task2;
    //                Object result = dynamicTask.Result;
    //                tcs2.SetResult(result);
    //            });
    //            return tcs2.Task;
    //        });

    //        task.ContinueWith(x =>
    //        {
    //            if (x.IsFaulted)
    //            {
    //                tcs.SetException(x.Exception);
    //                return;
    //            }

    //            tcs.SetResult((TResult)x.Result);
    //        });

    //        return tcs.Task;
    //    }
    //    void IInterceptor.Intercept(IInvocation invocation)
    //    {
    //        if (!typeof(Task).IsAssignableFrom(invocation.Method.ReturnType))
    //        {
    //            Intercept(invocation.InvocationTarget, invocation.Method, invocation.Arguments, invocation.Proceed);
    //            return;
    //        }
    //        var returnType = invocation.Method.ReturnType.IsGenericType ? invocation.Method.ReturnType.GetGenericArguments()[0] : typeof(object);
    //        var method = _taskCompletionSourceMethod.MakeGenericMethod(returnType);
    //        invocation.ReturnValue = method.Invoke(this, new object[] { invocation });
    //    }
    //}
}
