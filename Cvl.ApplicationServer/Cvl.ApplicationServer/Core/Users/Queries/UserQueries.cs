using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Users.Queries
{
    public class UserQueries
    {
        private readonly Repository<User> _repository;

        public UserQueries(Repository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _repository.GetAll().SingleAsync(x => x.UserEmail == email);
        }
    }
}
