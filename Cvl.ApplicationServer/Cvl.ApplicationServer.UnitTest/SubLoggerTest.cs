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
            using var logger = loggerFactory.GetLogger(message:"GetRequest");
            mainLogger = logger;
            logger.Trace("Request in GetRequest");
            requestLogics(logger);
        }

        private Logger mainLogger;

        private void requestLogics(Logs.Logger logger)
        {
            using var log = logger.GetSubLogger(message:"request logics");
            someFunction2(2);
            someFunction1(1);
            {
                using var sublog= log.GetSubLogger("tes tsubloga w request");
                sublog.Trace("trace subloga");
            }
            someFunction2(2);
        }

        private void someFunction1(int v)
        {
            using var log = mainLogger.GetSubLogger("fun1");
            someFunction2(10+v);
        }

        private void someFunction2(int v)
        {
            using var log = mainLogger.GetSubLogger("fun2");
            log.Trace($"fun2 in fun2 {v}");
            {
                using var sublog = log.GetSubLogger("test subloga w fun2");
                sublog.Trace("trace subloga fun2");
            }
        }
    }
}
