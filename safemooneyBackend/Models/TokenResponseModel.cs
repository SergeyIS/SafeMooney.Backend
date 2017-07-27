using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Models
{
    public class TokenResponseModel
    {
        public String Username { get; set; }

        public String Access_Token { get; set; }
    }
}