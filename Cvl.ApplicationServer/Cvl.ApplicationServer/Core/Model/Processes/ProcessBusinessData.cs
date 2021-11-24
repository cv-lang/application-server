using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
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
