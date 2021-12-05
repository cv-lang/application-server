using Cvl.ApplicationServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Test
{
    public class TestController
    {
        private readonly Core.ApplicationServer _applicationServer;

        public TestController(Core.ApplicationServer applicationServer)
        {
            _applicationServer = applicationServer;
        }


        public async Task<TestResponse> TestStep1Async(TestRequest request)
        {
            var process = await _applicationServer.CreateProcessAsync<ITestProcess>(GetConnectionData());

            await process.TestMethod1Async(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep2Async(TestRequest request)
        {
            var process = await _applicationServer.LoadProcessAsync<ITestProcess>(2,GetConnectionData());
            await process.TestMethod1Async(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep3Async(TestRequest request)
        {
            var process = await _applicationServer.LoadProcessAsync<ITestProcess>(2, GetConnectionData());
            process.TestMethod2WhitExeption(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep4Async(TestRequest request)
        {
            var process = await _applicationServer.LoadProcessAsync<ITestProcess>(2, GetConnectionData());
            await process.TestMethod3WithExeptionAsync(2);

            await Task.Delay(50);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep5Async(TestRequest request)
        {
            var process = await _applicationServer.LoadProcessAsync<ITestProcess>(2, GetConnectionData());
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
