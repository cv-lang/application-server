﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    [Table("StepHistory", Schema = "Processes")]
    public class StepHistory : BaseEntity
    {
        public StepHistory(long? processInstanceId, string step, string stepDescription)
        {
            ProcessInstanceId = processInstanceId;
            Step = step;
            StepDescription = stepDescription;
        }

        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstance? ProcessInstance { get; set; }
        public string Step { get; set; }
        public string StepDescription { get; set; }
    }
}
