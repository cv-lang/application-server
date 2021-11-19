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


        public async Task<TestResponse> TestMethod(TestRequest request)
        {
            var process = _applicationServer.CreateProcess<TestProcess>();
            process.TestMethod1(2);

            await Task.Delay(500);
            return new TestResponse();
        }
    }
}
