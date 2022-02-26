using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes;
using Cvl.ApplicationServer.Processes;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

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
                return (SimpleStepBaseTestProcessStep)ProcessData.Step;
            }
            set
            {
                ProcessData.Step = (int)value;
            }
        }

        private readonly IProcessManager _processManager;
        private readonly IApplicationServer _applicationServer;

        public SimpleStepBaseTestProcess(IProcessManager processManager,IApplicationServer applicationServer)
        {
            _processManager = processManager;
            _processManager.Process = this;
            _applicationServer = applicationServer;
            State = new SimpleStepBaseTestProcessState();
            State.StepName = "Start procesu";
        }

        public SimpleStepBaseTestProcessState State { get; set; }

        public void Step1FromApi()
        {
            _processManager.SetStep("step2", "step 2", SimpleStepBaseTestProcessStep.Step2);
            _processManager.SetToJobThread();
        }


        public void Step2FromJob()
        {
            _processManager.SetStep("Processing extrenral data", "Processing extrenral data", SimpleStepBaseTestProcessStep.Step3);

            var externalData = _processManager.GetExternalData(this.ProcessData.ProcessNumber);
           
            //ProcessData.SetStep($"externalData: {externalData}", "Processing extrenral data", SimpleStepBaseTestProcessStep.Step3);

            _processManager.SetToApiThread();
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
