﻿using Cvl.ApplicationServer.Core;
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
        private TestService _testService;

        public TestProcess(Core.ApplicationServer applicationServer, TestService testService)
            :base(applicationServer)
        {
            _testService = testService;
        }

        public async Task<int> TestMethod1Async(int i)
        {
            await SetStepAsync("Step", "Step descrption");

            return _testService.TestLogicMethod(i);
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

        public override void ProcessDeserialization(IFullSerializer serializer, string serializedProcess)
        {
            _processState = serializer.Deserialize<TestProcessState>(serializedProcess) ?? throw new Exception("Could not deserialize process state");
        }

        public override string ProcessSerizalization(IFullSerializer serializer)
        {
            return serializer.Serialize(_processState);
        }

        
    }
}
