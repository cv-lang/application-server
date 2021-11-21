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

    public interface ITestProcess : IProcess
    {
        int TestMethod1(int i);
    }

    public class TestProcess : BaseProcess, ITestProcess
    {
        private TestService _testService;

        public TestProcess(TestService testService)
        {
            _testService = testService;
        }

        public int TestMethod1(int i)
        {
            return _testService.TestLogicMethod(i);
        }

        public override void ProcessDeserialization(IFullSerializer serializer, string serializedProcess)
        {
            throw new NotImplementedException();
        }

        public override string ProcessSerizalization(IFullSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
