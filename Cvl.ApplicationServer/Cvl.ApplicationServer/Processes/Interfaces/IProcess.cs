﻿using Cvl.ApplicationServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface IProcess
    {
        long ProcessId { get; set; }
    }
}