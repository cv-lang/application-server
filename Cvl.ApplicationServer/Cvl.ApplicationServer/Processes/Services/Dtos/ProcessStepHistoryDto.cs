using Cvl.ApplicationServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Services.Dtos
{
    public class ProcessStepHistoryDto
    {
        public ProcessStepHistoryDto(ProcessStepHistory stepHistory)
        {
            StepHistory = stepHistory;
            Id = stepHistory.Id;
        }
        public ProcessStepHistoryDto() { }

        public ProcessStepHistory StepHistory { get; set; } = null!;
        public long Id { get; set; }
    }
}
