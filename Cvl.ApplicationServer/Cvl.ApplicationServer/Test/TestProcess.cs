using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Test
{
    public class TestProcessState
    {
        public string Request { get; set; }
        public int Dane { get; set; }
    }

    public interface ITestProcess : IProcess
    {
        Task<int> TestMethod1(int i);
        int TestMethod2(int i);
    }

    public class TestProcess : BaseProcess, ITestProcess
    {
        private TestProcessState _processState = new TestProcessState();
        private TestService _testService;

        public TestProcess(Core.ApplicationServer applicationServer, TestService testService)
            :base(applicationServer)
        {
            _testService = testService;
        }

        public async Task<int> TestMethod1(int i)
        {
            await SetStepAsync("Step", "Step descrption");

            return _testService.TestLogicMethod(i);
        }

        

        public int TestMethod2(int i)
        {
            throw new Exception("Jakiś tam błąd " + i);
            return _testService.TestLogicMethod(i);
        }

        public override void ProcessDeserialization(IFullSerializer serializer, string serializedProcess)
        {
            _processState = serializer.Deserialize<TestProcessState>(serializedProcess);
        }

        public override string ProcessSerizalization(IFullSerializer serializer)
        {
            return serializer.Serialize(_processState);
        }
    }
}
