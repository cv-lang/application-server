using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Users.Interfaces
{
    public interface IUsersService
    {
        void RegisterNewUser(string email, string password);
    }
}
