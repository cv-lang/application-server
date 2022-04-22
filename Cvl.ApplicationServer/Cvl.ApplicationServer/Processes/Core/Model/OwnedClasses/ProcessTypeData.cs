using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses
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
