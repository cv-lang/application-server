using Cvl.ApplicationServer.Contexts.Application;
using Cvl.ApplicationServer.Monitoring;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.Implementations.NetStandard20;

namespace Cvl.ApplicationServer.UnitTest.Monitoring
{
    public class MonitoringTest
    {
        [Test]
        public void TestLogowania()
        {
            ApplicationContext.Start(new FrameworkNetStandard20());

            var monitor = ApplicationContext.Instance.GetApplicationMonitor();
            using var logger = monitor.StartLogs("Strart metody TestLogowania")
                .AddParameter(1, "parameter1")
                .AddParameter("2","parameter2")
                .AddExpressionParameter(() => monitor);

            using var loger2 = monitor.GetSubLogger()
                .AddParameter(1,"testowyStałyParametr");

            TestSublogow(1, "234", monitor);


        }

        private void TestSublogow(int param1, string stringParam2, ApplicationMonitor monitor)
        {
            using var logger = monitor.GetSubLogger()
                .AddParameter(param1).AddParameter(stringParam2);

            logger.Trace("Test");
            logger.Info("Test");
        }
    }
}
