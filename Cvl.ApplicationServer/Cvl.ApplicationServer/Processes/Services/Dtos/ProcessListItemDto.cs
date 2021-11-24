using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Dtos
{
    public class ProcessListItemDto
    {       
        public ProcessListItemDto()
        { }

        public ProcessListItemDto(ProcessInstance processInstance)
        {
            Id = processInstance.Id;
            ProcessNumber = processInstance.ProcessNumber;
            TypeShortName = processInstance.Type.Split('.').LastOrDefault() ?? processInstance.Type;
            Type = processInstance.Type;
            StatusName = processInstance.StatusName;
            StepName = processInstance.StepName;
            StepDescription = processInstance.StepDescription + "jakiś opis";
            MainThreadState = processInstance.MainThreadState.ToString();
            BusinessData = processInstance.BusinessData;
            ExternalIds = processInstance.ExternalIds;
            NumberOfActivities = processInstance.ProcessDiagnosticData.NumberOfActivities;
            NumberOfSteps = processInstance.ProcessDiagnosticData.NumberOfSteps;
            NumberOfErrors = processInstance.ProcessDiagnosticData.NumberOfErrors;
            LastErrorPreview = processInstance.ProcessDiagnosticData.LastErrorPreview;
            LastRequestPreview = processInstance.ProcessDiagnosticData.LastRequestPreview;
            LastResponsePreview = processInstance.ProcessDiagnosticData.LastResponsePreview;
        }
        
        public long Id { get; set; }
        public string ProcessNumber { get; set; }
        public string Type { get; set; }
        public string TypeShortName { get; set; }
        public string StatusName { get; set; }
        public string StepName { get; set; }
        public string StepDescription { get; set; }
        public string MainThreadState { get; set; }

        public ProcessBusinessData BusinessData { get; set; } = null!;

        public ExternalIdentifiers ExternalIds { get; set; } = null!;

        public long NumberOfActivities { get; set; }
        public long NumberOfSteps { get; set; }
        public long NumberOfErrors { get; set; }
        public string? LastErrorPreview { get; set; }
        public string? LastRequestPreview { get; set; }
        public string? LastResponsePreview { get; set; }
    }  
}
