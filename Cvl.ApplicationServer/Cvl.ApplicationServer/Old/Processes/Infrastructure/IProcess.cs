﻿using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface IProcessOld
    {
        long ProcessId { get; set; }
        string ProcessNumber { get; set; }
    }
}