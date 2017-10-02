using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using SocialServicesLibrary.VkApi;
using SocialServicesLibrary.VkApi.Models;
using DataAccessLibrary;
using SharedResourcesLibrary;
using safemooneyBackend.Models;

namespace safemooneyBackend.Controllers
{
    public class ServicesController : ApiController
    {
        private IDataAccess db = new DataBuilder();

        [HttpGet]
        [Route("api/{userId}/services/vk/addservice")]
        public HttpResponseMessage AddService(String code, int userId)
        {

            VKAuthorization authorizator = new VKAuthorization();
            VKAuthorizationResponseModel response = authorizator.Authorize(code);

            if (response == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            AuthService service = db.GetServiceData(1, response.UserId);
            
            if(service == null)
            {
                AuthService userService = new AuthService()
                {
                    UserId = userId,
                    AuthId = response.UserId,
                    ProviderId = 1,
                    AuthToken = response.AccessToken,
                    AuthParam = null
                };

                if (!db.AddServiceData(userService))
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            else
            {
                service.AuthToken = response.AccessToken;
                if (!db.ChangeServiceData(service))
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
