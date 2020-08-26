using System;
using Cvl.ApplicationServer.Tools.Extension;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessDescription
    {
        public ProcessDescription()
        {
        }

        public ProcessDescription(ProcessContainer proc)
        {
            Id = proc.Process.Id;
            ProcessStatus = proc.Process.ProcessStatus;
            ProcessTypeFullName = proc.Process.GetType().FullName;
            ProcessTypeName = proc.Process.GetType().Name;
            ProcessTypeDescription = proc.Process.GetType().GetClassDescription();
            FormData = proc.Process.FormDataToShow;
            ProcessStep = proc.Process.ProcessStep;
            ProcessStepDescription = proc.Process.ProcessStepDescription;
            CreatedDate = proc.Process.CreatedDate;
            ParentId = proc.Process.ParentId;
            IterationNuber = proc.VirtualMachine.HardwareContext.NumerIteracji;
        }


        public long Id { get; set; }
        public EnumProcessStatus ProcessStatus { get; set; }
        public string ProcessTypeFullName { get;  set; }
        public string ProcessTypeName { get; set; }
        public string ProcessTypeDescription { get;  set; }
        public FormData FormData { get;  set; }
        public string ProcessStep { get; set; }
        public string ProcessStepDescription { get; set; }
        public DateTime CreatedDate { get;  set; }
        public long? ParentId { get;  set; }
        public long IterationNuber { get; set; }
    }
}
