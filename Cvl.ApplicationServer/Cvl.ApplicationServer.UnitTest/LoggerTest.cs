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

        public class TestPropertyClass
        {
            public string Id { get; set; } = "sdfsdf";
            public string Name { get; set; } = "Jan";
            public string Surname { get; set; } = "Kowalski";
            public string Address { get; set; } = "ul. Jana Paw³a II";
            public string PostCode { get; set; } = "11-222";
            public string City { get; set; } = "Katowice";

            public decimal D1 { get; set; } = 12.43m;

            public double D2 { get; set; } = 123.2;

            public string Description { get; set; } = "sd asdf asdf asdf wefw efwe sdf asd fasdf asdf asdf asdf asd fasdf asd fasd fa " +
                "s asdf asdf asdf asdf asdf asd fasd afsd fasd fasd fasdf asdf asdf asd fasdf asd fasd fasd fasd fasd fasd fsdf " +
                "sadf asdf asdf asdf asdf wefwefwefwefwefwef wef wef wef wef wef wef fasd"; 
        }

        private void anotherSubFuncion(int v1, int v2, string v, SubLogger logger)
        {
            using var log = logger.GetSubLogger();

            log.Trace("Ostatni log - najni¿szy poziom")
                .AddParameter(new TestPropertyClass())
                .AddParameter(new TestPropertyClass() { Id = "2", Description = "sdfsdfsd" }); ;
        }
    }
}