using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Model.OwnedClasses
{
    [Owned]
    public class ProcessThreadData
    {
        /// <summary>
        /// Thread state
        /// </summary>
        public Cvl.ApplicationServer.Processes.Threading.ThreadState MainThreadState { get; set; }

        /// <summary>
        /// Date of next main thread execution
        /// </summary>
        public DateTime? NextExecutionDate { get; set; }

    }
}
