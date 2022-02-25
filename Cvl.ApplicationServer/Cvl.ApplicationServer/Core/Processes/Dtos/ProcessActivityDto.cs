using Cvl.ApplicationServer.Core.Processes.Model;

namespace Cvl.ApplicationServer.Core.Processes.Dtos
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
