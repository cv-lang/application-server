using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
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
