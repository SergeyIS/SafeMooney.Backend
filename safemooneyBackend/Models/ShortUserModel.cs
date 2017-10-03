using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Models
{
    public class ShortUserModel
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool Availability { get; set; }
        public String AuthorizationId { get; set; }
    }
}