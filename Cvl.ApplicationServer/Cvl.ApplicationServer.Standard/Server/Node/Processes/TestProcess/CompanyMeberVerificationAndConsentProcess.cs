using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess
{
    [Description("Sub-process for verification company members and getting consent")]
    public class CompanyMeberVerificationAndConsentProcess : BaseProcess
    {
        protected override object Start(object inputParameter)
        {
            Log("Step 1 - get person data");
            SetStepData("person-data", "Get person data from company member board");
            var personData = new PersonData();
            ShowForm("GetPersonData", personData);

            Log("Step 2 - accept application");
            SetStepData("accept-application");
            var acceptApplication = getApplicationToAccept();
            ShowForm("AcceptApplication", acceptApplication);

            return null;
        }

        private ApplicationAcceptData getApplicationToAccept()
        {
            var app = new ApplicationAcceptData();

            return app;
        }
    }
}
