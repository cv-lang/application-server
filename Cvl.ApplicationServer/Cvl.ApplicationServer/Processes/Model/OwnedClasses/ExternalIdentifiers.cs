using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Model.OwnedClasses
{
    [Owned]
    public class ExternalIdentifiers
    {
        [StringLength(50)]
        public string? ExternalId1 { get; set; }

        [StringLength(50)]
        public string? ExternalId2 { get; set; }

        [StringLength(50)]
        public string? ExternalId3 { get; set; }

        [StringLength(50)]
        public string? ExternalId4 { get; set; }
    }
}
