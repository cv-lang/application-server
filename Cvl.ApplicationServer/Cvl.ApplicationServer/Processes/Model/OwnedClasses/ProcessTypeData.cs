using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Model.OwnedClasses
{
    [Owned]
    public class ProcessTypeData
    {
        /// <summary>
        /// Process full type name
        /// </summary>
        public string ProcessTypeFullName { get; set; }


        public ProcessType ProcessType { get; set; }
    }
}
