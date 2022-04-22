using Microsoft.EntityFrameworkCore;
using ThreadState = Cvl.ApplicationServer.Processes.Core.Threading.ThreadState;

namespace Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses
{
    [Owned]
    public class ProcessThreadData
    {
        /// <summary>
        /// Thread state
        /// </summary>
        public ThreadState MainThreadState { get; set; }

        /// <summary>
        /// Date of next main thread execution
        /// </summary>
        public DateTime? NextExecutionDate { get; set; }

    }
}
