using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;

namespace Cvl.ApplicationServer.Core.Users.Model
{
    public class User : BaseEntity
    {
        public User(string userName, string userEmail, string password, string salt, string roles)
        {
            UserName = userName;
            UserEmail = userEmail;
            Password = password;
            Salt = salt;
            Roles = roles;
        }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Roles { get; set; }
    }
}
