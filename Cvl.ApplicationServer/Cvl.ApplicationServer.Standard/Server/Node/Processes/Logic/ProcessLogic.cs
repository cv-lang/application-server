﻿using Cvl.ApplicationServer.Base;
using Cvl.ApplicationServer.Server.Node.Host;
using System;

namespace Cvl.ApplicationServer.Server.Node.Processes.Logic
{
    public class ProcessLogic : BaseLogic
    {
        private ApplicationServerNodeHost applicationServerNodeHost;

        public ProcessLogic(ApplicationServerNodeHost applicationServerNodeHost)
        {
            this.applicationServerNodeHost = applicationServerNodeHost;
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}
