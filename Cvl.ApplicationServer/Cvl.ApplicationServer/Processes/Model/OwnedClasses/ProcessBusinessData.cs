using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Model.OwnedClasses
{
    [Owned]
    public class ProcessBusinessData
    {
        public string? ClientName { get; set; }
        public string? VendorName { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
