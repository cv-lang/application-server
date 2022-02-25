using Cvl.ApplicationServer.Core.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.ApplicationServer.Core.Logging.Logger
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
