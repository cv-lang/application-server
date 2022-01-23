using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Users.Commands;
using Cvl.ApplicationServer.Core.Users.Interfaces;
using Cvl.ApplicationServer.Core.Users.Model;

namespace Cvl.ApplicationServer.Core.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserCommands _userCommands;

        public UsersService(UserCommands userCommands)
        {
            _userCommands = userCommands;
        }

        public async Task AddRootUserAsync()
        {
            await _userCommands.AddUserAsync(new User("root", "", "pswd", "1", ""));
        }

        public async Task RegisterNewUserAsync(string email, string password)
        {
            await _userCommands.AddUserAsync(new User("newUser", email, password, "1", ""));
        }
    }
}
