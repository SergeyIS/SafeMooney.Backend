using System;
using System.Net.Http;
using System.Web.Http;
using SocialServicesLibrary.VkApi;
using SharedResourcesLibrary;

namespace safemooneyBackend.Controllers
{
    public class OAuthController : ApiController
    {
        [HttpGet]
        [Route("api/oauth/authorize")]
        public HttpResponseMessage Authorize(String code)
        {
            //here is exception
            VKAuthorization vkAuthorization = new VKAuthorization(null);
            OAuthAuthorization userAuth = vkAuthorization.Authorize(code);
            
            //search database

            return Request.CreateResponse(userAuth);
        }
    }
}
