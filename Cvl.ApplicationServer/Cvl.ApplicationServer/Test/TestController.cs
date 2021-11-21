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


        public async Task<TestResponse> TestStep1(TestRequest request)
        {
            var process = _applicationServer.CreateProcess<TestProcess,ITestProcess>(GetConnectionData());

            process.TestMethod1(2);

            await Task.Delay(500);
            return new TestResponse();
        }

        public async Task<TestResponse> TestStep2(TestRequest request)
        {
            //var process = _applicationServer.CreateProcess<TestProcess>(GetConnectionData());
            //process.TestMethod1(2);

            await Task.Delay(500);
            return new TestResponse();
        }

        public ClientConnectionData GetConnectionData()
        {
            return new ClientConnectionData("localTest","1","");
        }
    }
}
