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
        [Route("api/oauth/vk/authorize")]
        public HttpResponseMessage Authorize(String code)
        {
            try
            {
                VKAuthorization vkAuthorizationClient = new VKAuthorization();
                VKAuthorizationResponseModel userAuth = vkAuthorizationClient.Authorize(code);

                if (userAuth == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                //search database for user
                AuthService service = db.GetServiceData(1, userAuth.UserId);

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
                    VKUsers vkUsersClient = new VKUsers(userAuth.AccessToken);
                    VKUserModel vkUser = vkUsersClient.GetUserData(userAuth.UserId);

                    if (vkUser == null)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);

                    //mathing by email
                    if (!String.IsNullOrEmpty(vkUser.Email))
                    {
                        //todo: search users by email from vkUser
                    }
                    else
                    {
                        //there is not user with such email in the database
                        //add new user
                        db.AddUserSafely(vkUser.UserId, userAuth.AccessToken, vkUser.FirstName, vkUser.LastName);
                        User newUser = db.FindUserByLogin(vkUser.UserId);

                        //add new social service for new user
                        bool resultOfOperation = db.AddServiceData(new AuthService() {
                            AuthId = vkUser.UserId,
                            UserId = newUser.Id,
                            ProviderId = 1,
                            AuthToken = userAuth.AccessToken,
                            AuthParam = null
                        });

                        if (!resultOfOperation)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError);

                        TokenResponseModel response = new TokenResponseModel()
                        {
                            UserId = newUser.Id,
                            Username = newUser.Username,
                            FirstName = newUser.FirstName,
                            LastName = newUser.LastName,
                            Access_Token = userAuth.AccessToken
                        };

                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }


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
