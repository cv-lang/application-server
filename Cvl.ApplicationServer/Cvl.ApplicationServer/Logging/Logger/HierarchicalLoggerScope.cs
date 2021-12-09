using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Logging.Logger
{
    internal class HierarchicalLoggerScope : IDisposable
    {
        private HierarchicalLogger hierarchicalLogger;

        private string state;

        public HierarchicalLoggerScope(HierarchicalLogger hierarchicalLogger, object state)
        {
            this.hierarchicalLogger = hierarchicalLogger;
            this.state = state?.ToString();
        }

        public void Dispose()
        {
            hierarchicalLogger.EndScope();
        }

        public override string ToString()
        {
            return state.ToString();
        }
    }
}
