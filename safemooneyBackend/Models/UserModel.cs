using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Models
{
    public class UserModel
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}