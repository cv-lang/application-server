﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Interfaces;
using Cvl.ApplicationServer.Processes;
using ThreadState = Cvl.ApplicationServer.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Test
{
    public enum SimpleStepBaseTestProcessStep
    {
        Init=0,
        Step1=1,
        Step2=2,
        Step3=3,
    }
    public class SimpleStepBaseTestProcessState
    {
        public string StepName { get; set; }
    }

    public class SimpleStepBaseTestProcess : BaseProcess
    {
        protected SimpleStepBaseTestProcessStep Step
        {
            get
            {
                return (SimpleStepBaseTestProcessStep)ProcessData.ProcessInstanceContainer.Step.Step;
            }
            set
            {
                ProcessData.ProcessInstanceContainer.Step.Step = (int)value;
            }
        }

        private readonly IApplicationServer _applicationServer;

        public SimpleStepBaseTestProcess(IApplicationServer applicationServer)
        {
            _applicationServer = applicationServer;
            State = new SimpleStepBaseTestProcessState();
            State.StepName = "Start procesu";
        }

        public SimpleStepBaseTestProcessState State { get; set; }

        public void Step1FromApi()
        {
            ProcessData.SetStep("step2", "step 2", SimpleStepBaseTestProcessStep.Step2);
            ProcessData.SetToJobThread();
        }


        public void Step2FromJob()
        {
            State.StepName = "Processing extrenral data";
            var externalData = _applicationServer.Processes.GetExternalDataInput(this.ProcessData.ProcessNumber);
            State.StepName = $"externalData: {externalData}";
        }

        public override void JobEntry()
        {
            switch (Step)
            {
                case SimpleStepBaseTestProcessStep.Step1:
                    break;
                case SimpleStepBaseTestProcessStep.Step2:
                    Step2FromJob();
                    break;
                case SimpleStepBaseTestProcessStep.Step3:
                    break;
            }
        }

        public override object GetProcessState()
        {
            return State;
        }

        public override void LoadProcessState(object processState)
        {
            State = (SimpleStepBaseTestProcessState)processState;
        }

        
    }
}
