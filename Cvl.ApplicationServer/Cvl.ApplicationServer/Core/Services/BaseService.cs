using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        //protected readonly Repositories.Repository<T> Repository;

        public BaseService(ApplicationDbContext applicationDbContext)
        {
            //Repository = new Repositories.Repository<T>(applicationDbContext);
        }
    }
}
