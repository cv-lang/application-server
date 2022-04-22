using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses
{
    [Owned]
    public class ProcessSpecificData
    {
        public string? ProcessSpecificData1 { get; set; }
        public string? ProcessSpecificData2 { get; set; }
    }
}
