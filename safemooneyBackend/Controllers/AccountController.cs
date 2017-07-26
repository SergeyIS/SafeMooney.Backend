using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using safemooneyBackend.Filters;
using DataAccessLibrary;
using SharedResourcesLibrary.AccountResources;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;

namespace safemooneyBackend.Controllers
{
    public class AccountController : ApiController
    {
        private DataStorageEmulator db = new DataStorageEmulator();

        [HttpGet]
        [Route("api/{username}/account/login/{pas}")]
        public HttpResponseMessage LogIn(String username, String pas)
        {
            User user = db.FindUserByLogin(username);
            if (user == null || !user.Password.Equals(pas))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            var response = Request.CreateResponse<String>(HttpStatusCode.OK, "It's OK");
            return response;
        }
    }
}