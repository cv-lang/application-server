using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Temporary;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Logging.Logger
{
    /// <summary>
    /// Zarządza logami
    /// </summary>
    public class LogManager
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IServiceProvider serviceProvider;
        private long _currentLogId = 0;

        public LogManager(IServiceProvider serviceProvider)
        {
            //this.scopeFactory = scopeFactory;
            this.serviceProvider = serviceProvider;
        }

        public void Test()
        {
            var dbOptions = serviceProvider.GetService<DbContextOptions<ApplicationServerDbContext>>();
            using (var db = new ApplicationServerDbContext(dbOptions))
            {
                try
                {

                }
                catch (Exception ex)
                {

                }

                // when we exit the using block,
                // the IServiceScope will dispose itself 
                // and dispose all of the services that it resolved.
            }
        }
    }
}
