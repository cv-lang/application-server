using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Users.Model;

namespace Cvl.ApplicationServer.Core.Users.Commands
{
    internal class UserCommands
    {
        private readonly Repository<User> _repository;

        public UserCommands(Repository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsync(User user)
        {
            _repository.Insert(user);
            await _repository.SaveChangesAsync();
        }
    }
}
