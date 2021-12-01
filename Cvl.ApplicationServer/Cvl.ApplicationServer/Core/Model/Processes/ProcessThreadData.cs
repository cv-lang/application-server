using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
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
