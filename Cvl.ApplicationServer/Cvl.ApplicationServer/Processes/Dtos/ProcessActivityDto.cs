using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Services.Dtos
{
    public class ProcessActivityDto
    {
        public ProcessActivityDto(ProcessActivity activity)
        {
            Activity = activity;
            Id = activity.Id;
        }
        public ProcessActivityDto() { }

        public ProcessActivity Activity { get; set; } = null!;
        public long Id { get; set; }
    }
}
