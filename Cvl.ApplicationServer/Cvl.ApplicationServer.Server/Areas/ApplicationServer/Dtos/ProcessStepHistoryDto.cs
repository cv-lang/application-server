using Cvl.ApplicationServer.Processes.Core.Model;

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
