using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses;

namespace Cvl.ApplicationServer.Core.Processes.Dtos
{
    public class ProcessListItemDto
    {       
        public ProcessListItemDto()
        { 
        }

        public ProcessListItemDto(ProcessInstanceContainer processInstance)
        {
            Id = processInstance.Id;
            ProcessNumber = processInstance.ProcessNumber;
            TypeShortName = processInstance.ProcessTypeData.ProcessTypeFullName
                .Split(',').First()
                .Split('.').LastOrDefault() ?? processInstance.ProcessTypeData.ProcessTypeFullName;
            Type = processInstance.ProcessTypeData.ProcessTypeFullName;
            StatusName = processInstance.StatusName;
            StepName = processInstance.Step.StepName;
            StepDescription = processInstance.Step.StepDescription + "jakiś opis";
            MainThreadState = processInstance.ThreadData.MainThreadState.ToString();
            BusinessData = processInstance.BusinessData;
            ExternalIds = processInstance.ExternalIds;
            NumberOfActivities = processInstance.ProcessDiagnosticData.NumberOfActivities;
            NumberOfSteps = processInstance.ProcessDiagnosticData.NumberOfSteps;
            NumberOfErrors = processInstance.ProcessDiagnosticData.NumberOfErrors;
            LastErrorPreview = processInstance.ProcessDiagnosticData.LastErrorPreview;
            LastRequestPreview = processInstance.ProcessDiagnosticData.LastRequestPreview;
            LastResponsePreview = processInstance.ProcessDiagnosticData.LastResponsePreview;
            ProcessSpecificData = processInstance.ProcessSpecificData;
            CreatedDate = processInstance.CreatedDate.ToShortDateString();
            CreatedDateTime = processInstance.CreatedDate.ToString();
            ModifiedDate = processInstance.ModifiedDate.ToShortDateString();
            ModifiedDateTime = processInstance.ModifiedDate.ToString();
        }
        public long Id { get; set; }
        public string ProcessNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string TypeShortName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string StepName { get; set; } = string.Empty;
        public string StepDescription { get; set; } = string.Empty;
        public string MainThreadState { get; set; } = string.Empty;

        public ProcessBusinessData BusinessData { get; set; } = null!;

        public ExternalIdentifiers ExternalIds { get; set; } = null!;

        public ProcessSpecificData ProcessSpecificData { get; } = null!;

        public long NumberOfActivities { get; set; }
        public long NumberOfSteps { get; set; }
        public long NumberOfErrors { get; set; }
        public string? LastErrorPreview { get; set; }
        public string? LastRequestPreview { get; set; }
        public string? LastResponsePreview { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedDateTime { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedDateTime { get; set; }
    }
}
