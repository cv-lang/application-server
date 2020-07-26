using Cvl.ApplicationServer.Monitoring;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.UnitTest.Monitoring
{
    public class MonitoringTest
    {
        [Test]
        public void TestLogowania()
        {
            var monitor = new ApplicationMonitor(new Contexts.Application.ApplicationContext(), "");
            using var loger = monitor.StartLogs("Strart metody TestLogowania")
                .AddParameter(1, "parameter1")
                .AddParameter("2","parameter2")
                .AddExpressionParameter(() => monitor);

            using var loger2 = monitor.GetSubLogger()
                .AddParameter(1,"testowyStałyParametr");

            
            
        }
    }
}
