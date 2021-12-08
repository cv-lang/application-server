using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
{
    [Owned]
    public class ProcessStepData
    {        
        public ProcessStepData(int? step, string stepName, string stepDescription)
        {
            Step = step;
            StepName = stepName;
            StepDescription = stepDescription;
        }

        /// <summary>
        /// process step
        /// </summary>
        public int? Step { get; set; }
        /// <summary>
        /// User-friendly process step name
        /// </summary>
        public string StepName { get; set; } = "new";

        /// <summary>
        /// User-friendly process step description
        /// </summary>
        public string StepDescription { get; set; } = "new";
    }
}
