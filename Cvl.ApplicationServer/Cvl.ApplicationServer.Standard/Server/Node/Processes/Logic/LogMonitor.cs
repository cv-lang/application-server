using Cvl.ApplicationServer.Server.Node.Processes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Cvl.VirtualMachine.Core.Tools;
using System.Reflection;

namespace Cvl.ApplicationServer.Server.Node.Processes.Logic
{
    public class LogMonitor : ILogMonitor
    {
        public BaseProcess Process;

        public LogMonitor(){}
        public LogMonitor(BaseProcess process)
        {
            this.Process = process;
        }

        public void EventCall(MethodBase method, List<object> parameters, int callLevel, long iterationNumber)
        {
            var n = new StringBuilder();
            for (int i = 0; i < callLevel; i++)
            {
                n.Append(" ");
            }

            string para = "";
            foreach (object parameter in parameters)
            {
                var p = parameter?.ToString() ?? "null";
                if (p.Length > 100)
                {
                    p = p.Substring(0, 99);
                }

                para += p + ", ";
            }

            Process.ProcessLog.Logger.Trace($"{n.ToString()} {method.Name}")
                .AddParameter(callLevel,"callLevel")
                .AddParameter(iterationNumber,"iterationNumber");
        }

        public void EventRet(object ret, long iterationNumber)
        {
            if( ret is Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs.ProcessLog)
            {
                Process.ProcessLog.Logger.Trace($"Powrót z metody")
                    .AddParameter("referencja usunięta", "ProcessLog");
                return;
            }

            Process.ProcessLog.Logger.Trace($"Powrót z metody")
                .AddParameter(ret, "zwrócona wartość")
                .AddParameter(iterationNumber, "iterationNumber");
        }
    }
}
