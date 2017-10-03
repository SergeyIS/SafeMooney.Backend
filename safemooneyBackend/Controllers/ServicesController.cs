using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using SocialServicesLibrary.VkApi;
using SocialServicesLibrary.VkApi.Models;
using DataAccessLibrary;
using SharedResourcesLibrary;
using safemooneyBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace safemooneyBackend.Controllers
{
    public class ServicesController : ApiController
    {
        private IDataAccess db = null;

        public ServicesController()
        {
            db = new DataBuilder();
        }

        /// <summary>
        /// This method add service for user into AuthServices table
        /// </summary>
        /// <param name="code">Parametr that returned from vk.com</param>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/services/vk/addservice")]
        public HttpResponseMessage AddService(String code, int userId)
        {
            try
            {
                VKAuthorization authorizator = new VKAuthorization();
                VKAuthorizationResponseModel response = authorizator.Authorize(code);

                if (response == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                AuthService service = db.GetServiceData(1, response.UserId);

                if (service == null)
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
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        [HttpGet]
        [Route("api/{userId}/services/vk/search")]
        public HttpResponseMessage Search(String query, int userId)
        {
            if (String.IsNullOrEmpty(query))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                //getting data about user with userId
                User currentUser = db.FindUserById(userId);

                if (currentUser == null || currentUser.TokenKey == null)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);

                //getting data about social service for user with userId
                AuthService service = db.FindServiceByUserId(userId);

                if (service == null || String.IsNullOrEmpty(service.AuthId) || String.IsNullOrEmpty(service.AuthToken))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No social service was found");

                //searching friends in vk.com
                VKFriends friendsClient = new VKFriends();
                VKFriendsResponseModel friendsResponse = friendsClient.Search(service.AuthId, service.AuthToken, query);

                if (friendsResponse == null)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);

                if (friendsResponse.Response == null || friendsResponse.Response.Items == null || friendsResponse.Response.Items.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, "Nothing to show");

                //create response list
                List<ShortUserModel> userResponseList = new List<ShortUserModel>(friendsResponse.Response.Items.Count);

                //verifying users which are consist in database
                foreach (var vkuser in friendsResponse.Response.Items)
                {
                    try
                    {
                        AuthService localService = db.FindServiceByAuthId(vkuser.UserId);
                        if(localService != null)
                        {
                            User localUser = db.FindUserById(localService.UserId);
                            if (localUser == null) //internal error
                                continue;

                            userResponseList.Add(new ShortUserModel()
                            {
                                FirstName = localUser.FirstName,
                                LastName = localUser.LastName,
                                Username = localUser.Username,
                                UserId = localUser.Id,
                                AuthorizationId = vkuser.UserId,
                                Availability = true
                            });
                        }
                        else
                        {
                            userResponseList.Add(new ShortUserModel()
                            {
                                FirstName = vkuser.FirstName,
                                LastName = vkuser.LastName,
                                Username = vkuser.FirstName,
                                AuthorizationId = vkuser.UserId,
                                Availability = false
                            });
                        }
                    }
                    catch(Exception e)
                    {
                        //todo: write log
                        continue;
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, userResponseList);

            }
            catch (Exception e)
            {
                //todo: write log
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
