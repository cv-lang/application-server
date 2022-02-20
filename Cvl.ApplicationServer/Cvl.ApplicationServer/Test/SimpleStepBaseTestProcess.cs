using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Test
{
    public enum SimpleStepBaseTestProcessStep
    {
        Init=0,
        Step1=1,
    }
    public class SimpleStepBaseTestProcessState
    {
        public string StepName;
    }

    public class SimpleStepBaseTestProcess : BaseProcess
    {
        public SimpleStepBaseTestProcess()
        {
            State = new SimpleStepBaseTestProcessState();
        }

        public SimpleStepBaseTestProcessState State { get; set; }

        public override object GetProcessState()
        {
            throw new NotImplementedException();
        }

        public override void LoadProcessState(object processState)
        {
            throw new NotImplementedException();
        }
    }
}
