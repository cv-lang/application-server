using Cvl.ApplicationServer.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Test
{
    public class TestController
    {
        private readonly Core.ApplicationServerOld _applicationServer;
        private readonly ILogger<TestController> _logger;

        public TestController(Core.ApplicationServerOld applicationServer, ILogger<TestController> logger)
        {
            _applicationServer = applicationServer;
            this._logger = logger;
        }


        public async Task<TestResponse> TestStep1Async(TestRequest request)
        {
            using var log = _logger.BeginScope("TestStep1Async");

            _logger.LogWarning("Create process");
            var process = await _applicationServer.Processes.CreateProcessAsync<ITestProcess>(GetConnectionData());


            _logger.LogWarning("Invoke process.TestMethod1Async");
            await process.TestMethod1Async(2);

            await Task.Delay(50);
            return new TestResponse() { ProcessNumber = process.ProcessNumber};
        }

        public async Task<TestResponse> TestStep2Async(TestRequest request)
        {
            var process = await _applicationServer.Processes.LoadProcessAsync<ITestProcess>(request.ProcessNumber, GetConnectionData());
            await process.TestMethod1Async(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep3Async(TestRequest request)
        {
            var process = await _applicationServer.Processes.LoadProcessAsync<ITestProcess>(request.ProcessNumber, GetConnectionData());
            process.TestMethod2WhitExeption(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep4Async(TestRequest request)
        {
            var process = await _applicationServer.Processes.LoadProcessAsync<ITestProcess>(request.ProcessNumber, GetConnectionData());
            await process.TestMethod3WithExeptionAsync(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep5Async(TestRequest request)
        {
            var process = await _applicationServer.Processes.LoadProcessAsync<ITestProcess>(2, GetConnectionData());
            await process.TestMethod4WithExeptionAsync(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public ClientConnectionData GetConnectionData()
        {
            return new ClientConnectionData("localTest","1","");
        }
    }
}
