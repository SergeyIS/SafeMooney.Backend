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
    public class OAuthController : ApiController
    {
        private IDataAccess db = new DataBuilder();

        [HttpGet]
        [Route("api/oauth/authorize")]
        public HttpResponseMessage Authorize(String code)
        {
            try
            {
                VKAuthorization vkAuthorization = new VKAuthorization();
                VKAuthorizationResponse userAuth = vkAuthorization.Authorize(code);

                if (userAuth == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                //search database for user
                AuthService service = db.GetServiceData(0, userAuth.UserId);

                if(service != null)
                {
                    //applying new access_token to servise data
                    bool resultOfOperation = db.ChangeServiceData(service);

                    if (!resultOfOperation)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);

                    //find user with service.UserId
                    User localUser = db.FindUserById(service.UserId);

                    if(localUser == null)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);

                    TokenResponseModel response = new TokenResponseModel()
                    {
                        UserId = localUser.Id,
                        Username = localUser.Username,
                        FirstName = localUser.FirstName,
                        LastName = localUser.LastName,
                        Access_Token = localUser.TokenKey
                    };
                    

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    //todo: getting data about user from social service and creating new user in data base
                    return Request.CreateResponse(HttpStatusCode.OK, "Your account is not found. We need to create it before you'll can authorize with social service");

                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
