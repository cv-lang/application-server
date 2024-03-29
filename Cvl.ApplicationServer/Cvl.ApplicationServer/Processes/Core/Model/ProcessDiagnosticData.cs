﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Cvl.ApplicationServer.Core.DataLayer.Model;

namespace Cvl.ApplicationServer.Processes.Core.Model
{
    [Table("ProcessDiagnosticData", Schema = "Processes")]
    public class ProcessDiagnosticData : BaseEntity
    {
        public long ProcessInstanceId { get; set; }

        [JsonIgnore]
        public virtual ProcessInstanceContainer ProcessInstance { get; set; } = null!;

        /// <summary>
        /// How much activity has been performed
        /// </summary>
        public long NumberOfActivities { get; set; }

        /// <summary>
        /// How many steps has been performed
        /// </summary>
        public long NumberOfSteps { get; set; }

        /// <summary>
        /// How many errors has been thrown
        /// </summary>
        public long NumberOfErrors { get; set; }

        public string? LastError { get; set; }
        public string? LastErrorPreview { get; set; }
        public string? LastRequestPreview { get; set; } = null!;
        public string? LastResponsePreview { get; set; }
    }
}
