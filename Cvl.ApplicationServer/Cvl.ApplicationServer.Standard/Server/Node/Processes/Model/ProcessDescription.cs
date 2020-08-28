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
            ProcessInstanceDescription = proc.Process.ProcessInstanceDescription;
            ProcessInstanceFullDescription = proc.Process.ProcessInstanceFullDescription;
            ProcessParameter = proc.Process.ProcessParameter;
            ProcessParameter2 = proc.Process.ProcessParameter2;
            ExecutionDate=proc.Process.ExecutionDate;
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
        public string ProcessInstanceDescription { get; set; }
        public string ProcessInstanceFullDescription { get; set; }
        public string ProcessParameter { get; set; }
        public string ProcessParameter2 { get; set; }
        public DateTime ExecutionDate { get;  set; }
    }
}
