using Cvl.ApplicationServer.Contexts.Application.LogAbstraction;
using Cvl.ApplicationServer.Contexts.FrameworkAbstractions;
using Cvl.ApplicationServer.Monitoring.Base;
using Cvl.ApplicationServer.Monitoring.Base.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.Application.LogStorageAbstraction
{
    public class LiteDBLogStorage : ILogStorageAbstraction
    {
        private ApplicationContext applicationContext;        

        public LiteDBLogStorage(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
            applicationContext.Framework.IO.CreateDirectory("c:\\cvl\\logs\\");
        }

        public void FlushLogger(Logger logger)
        {
            
            using (var db = new LiteDatabase(@"c:\\cvl\\logs\\node-logs.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<LogModel>("Logs");
                                
                // Insert new customer document (Id will be auto-incremented)
                col.Insert(logger.Logs);               

                
            }
        }
    }
}
