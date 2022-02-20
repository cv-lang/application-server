using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Model.OwnedClasses;

namespace Cvl.ApplicationServer.Processes.Dtos
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
            TypeShortName = processInstance.ProcessTypeData.ProcessTypeFullName.Split('.').LastOrDefault() ?? processInstance.ProcessTypeData.ProcessTypeFullName;
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
        
    }  
}
