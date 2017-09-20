using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace safemooneyBackend.Controllers
{
    public class OAuthController : ApiController
    {
        [HttpGet]
        [Route("api/oauth/authorize")]
        public HttpResponseMessage Authorize(String code)
        {
            throw new NotImplementedException();
        }
    }
}
