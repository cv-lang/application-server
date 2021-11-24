﻿using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessStepHistoryRepository : Repository<ProcessStepHistory>
    {
        public ProcessStepHistoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
