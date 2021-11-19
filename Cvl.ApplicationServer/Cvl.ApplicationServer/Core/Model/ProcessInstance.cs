using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{

    /// <summary>
    /// Process instances
    /// </summary>
    public class ProcessInstance : BaseEntity
    {
        /// <summary>
        /// User-friendly process number
        /// </summary>
        public string ProcessNumber { get; set; }

        /// <summary>
        /// Process full name type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// User-friendly process status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// User-friendly process step name
        /// </summary>
        public string Step { get; set; }

        /// <summary>
        /// User-friendly process step description
        /// </summary>
        public string StepDescription { get; set; }

        /// <summary>
        /// Main(single) thread state 
        /// </summary>
        public ThreadState MainThreadState { get; set; }
    }
}
