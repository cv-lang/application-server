using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Jobs.Model
{
    /// <summary>
    /// Obiekt opisujący joba wykonywanego na serwerze
    /// </summary>
    public class JobDescription
    {
        /// <summary>
        /// Job name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Job description
        /// </summary>
        public string Description { get; set; }
    }
}
