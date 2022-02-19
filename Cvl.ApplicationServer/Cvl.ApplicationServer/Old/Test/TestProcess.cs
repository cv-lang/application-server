using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Threading;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Test
{
    public enum TestProcessStep
    {
        Init,
        Test1,
        Test2,
    }
    public class TestProcessState
    {
        public string Request { get; set; } = string.Empty;
        public int Dane { get; set; }
    }

    public interface ITestProcess : IProcess
    {
        Task<int> TestMethod1Async(int i);
        int TestMethod2WhitExeption(int i);

        Task<int> TestMethod3WithExeptionAsync(int i);

        Task TestMethod4WithExeptionAsync(int i);
    }

    public class TestProcess : BaseProcess, ITestProcess
    {
        private TestProcessState _processState = new TestProcessState();
        private readonly ILogger<TestProcess> _logger;
        private TestService _testService;

        public TestProcess(ApplicationServers.ApplicationServer applicationServer, ILogger<TestProcess> logger,  TestService testService)
            :base()
        {
            this._logger = logger;
            _testService = testService;
        }

        public async Task<int> TestMethod1Async(int i)
        {
            using var log = _logger.BeginScope("TestMethod1Async");

            _logger.LogWarning("Krok 1");
            ProcessData.SetStep("Step", "Step descrption", TestProcessStep.Init);

            var container = ProcessData.ProcessInstanceContainer;

            _logger.LogWarning("Krok 2");
            container.ExternalIds.ExternalId1 = "4443332211";
            container.ExternalIds.ExternalId2 = "+44111222333";
            container.ExternalIds.ExternalId3 = "41";
            container.ExternalIds.ExternalId4 = "342";
            container.BusinessData.VendorName = "vendorTest";
            container.BusinessData.ClientName = "CLient Test";
            container.BusinessData.Email = "test@clientTest.com";
            container.BusinessData.Phone = "+48333222111";
            
            _logger.LogWarning("Krok 3");
            testMetod();
            _logger.LogWarning("Krok 4");
            testMetod();

            return _testService.TestLogicMethod(i);
        }

        private void testMetod()
        {
            using var log = _logger.BeginScope("testMetod");
            _logger.LogWarning("testowy 1");

            _logger.LogWarning("testowy 2");
        }
        

        public int TestMethod2WhitExeption(int i)
        {
            throw new Exception("Błąd TestMethod2WhitExeption" + i);
            //return _testService.TestLogicMethod(i);
        }

        public async Task<int> TestMethod3WithExeptionAsync(int i)
        {
            if (i == 0)
            {
                throw new Exception("Błąd TestMethod3WithExeptionAsync");
            }

            await Task.Delay(50);

            return i;
        }

        public async Task TestMethod4WithExeptionAsync(int i)
        {
            if (i == 0)
            {
                throw new Exception("Błąd TestMethod3WithExeptionAsync");
            }

            await Task.Delay(50);
        }

        public override object GetProcessState()
        {
            return _processState;
        }

        public override void LoadProcessState(object processState)
        {
            _processState = (TestProcessState)processState;
        }
    }
}
