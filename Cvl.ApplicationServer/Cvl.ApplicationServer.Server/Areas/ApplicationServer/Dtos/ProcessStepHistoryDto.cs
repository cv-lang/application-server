using Cvl.ApplicationServer.Core.Processes.Model;

namespace Cvl.ApplicationServer.Core.Processes.Dtos
{
    public class ProcessStepHistoryDto
    {
        internal ProcessStepHistoryDto(ProcessStepHistory stepHistory)
        {
            StepHistory = stepHistory;
            Id = stepHistory.Id;
        }
        public ProcessStepHistoryDto() { }

        public ProcessStepHistory StepHistory { get; set; } = null!;
        public long Id { get; set; }
    }
}
