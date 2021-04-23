using Cvl.ApplicationServer.Logs;
using Cvl.ApplicationServer.Logs.Factory;
using NUnit.Framework;
using System;
using System.Linq;

namespace Cvl.ApplicationServer.UnitTest
{
    public class LoggerTest
    {
        private const string module = "TestModule";
        private LoggerFactory loggerFactory;
        [SetUp]
        public void Setup()
        {
            loggerFactory = new LoggerFactory(new Logs.Storage.FileLogStorage(), module);
        }

        [Test]
        public void Test1()
        {
            var externalId = DateTime.Now.Ticks.ToString();
            using (var log = loggerFactory.GetLogger(externalId))
            {
                log.Info("test info").AddParameter(1, "pramet 1 - 1");
                someSubfunction(1, 2, "3", log);
            }

            using (var log = loggerFactory.GetLogger(externalId))
            {
                log.Info("test info").AddParameter(1, "pramet 1 - 1");
                someSubfunction(1, 2, "3", log);
            }

            var lastHeader = loggerFactory.LogStorage.GetHeaders().Last();
            var lastFull = loggerFactory.LogStorage.GetLogElement(lastHeader.UniqueId);

            var idTest = lastFull.Elements.Last().Elements.Last().UniqueId;

            var l = loggerFactory.LogStorage.GetLogElement(idTest);
        }

        private void someSubfunction(int v1, int v2, string v3, Logger logger)
        {
            using var log = logger.GetSubLogger();
            log.Trace("jakieœ tam info");

            anotherSubFuncion(v1, v2, "test", log);
        }

        private void anotherSubFuncion(int v1, int v2, string v, SubLogger logger)
        {
            using var log = logger.GetSubLogger();

            log.Trace("Ostatni log - najni¿szy poziom");
        }
    }
}