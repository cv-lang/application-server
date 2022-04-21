using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses
{
    [Owned]
    public class ProcessSpecificData
    {
        public string? ProcessSpecificData1 { get; set; }
        public string? ProcessSpecificData2 { get; set; }
    }
}
