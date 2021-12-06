using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
{
    [Owned]
    public class ProcessSpecificData
    {
        public string? ProcessSpecificData1 { get; set; }
        public string? ProcessSpecificData2 { get; set; }
    }
}
