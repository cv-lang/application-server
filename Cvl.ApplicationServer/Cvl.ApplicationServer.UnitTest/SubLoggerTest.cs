using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Logs;
using Cvl.ApplicationServer.Logs.Factory;
using Cvl.ApplicationServer.Logs.Storage;
using NUnit.Framework;

namespace Cvl.ApplicationServer.UnitTest
{
    public class SubLoggerTest
    {
        private LoggerFactory loggerFactory;
        [SetUp]
        public void Setup()
        {
            loggerFactory = new LoggerFactory(new FileLogStorage("c:\\logs\\"), "Test");
        }

        [Test]
        public void GetRequest()
        {
            using var logger = loggerFactory.GetLogger();
            mainLogger = logger;
            logger.Trace("Request");
            requestLogics(logger);
        }

        private Logger mainLogger;

        private void requestLogics(Logs.Logger logger)
        {
            using var log = logger.GetSubLogger();
            someFunction2(2);
            someFunction1(1);
            someFunction2(2);
        }

        private void someFunction1(int v)
        {
            using var log = mainLogger.GetSubLogger();
            someFunction2(10+v);
        }

        private void someFunction2(int v)
        {
            using var log = mainLogger.GetSubLogger();
            log.Trace($"fun2 {v}");
        }
    }
}
