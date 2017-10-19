using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using SocialServicesLibrary.VkApi;
using SocialServicesLibrary.VkApi.Models;
using DataAccessLibrary;
using SharedResourcesLibrary;
using SharedResourcesLibrary.Models;
using safemooneyBackend.Models;
using System.Collections.Generic;
using safemooneyBackend.Security.Filters;
using SocialServicesLibrary.Email;
using SocialServicesLibrary.Email.Models;
using System.IO;
using NLog;

namespace safemooneyBackend.Controllers
{
    public class ServicesController : ApiController
    {
        private IDataAccess db = null;
        private Logger logger = null;
        public ServicesController()
        {
            db = new DataBuilder();
            logger = LogManager.GetCurrentClassLogger();
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
                //write log
                logger.Info("connecting vk.com to get access_token");

                VKAuthorization authorizator = new VKAuthorization();
                VKAuthorizationResponseModel response = authorizator.Authorize(code);

                if (response == null)
                {
                    logger.Warn($"user {userId} isn't authorized by vk.com");
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                    

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
                    {
                        logger.Error("could not add service");
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                        
                }
                else
                {
                    service.AuthToken = response.AccessToken;
                    if (!db.ChangeServiceData(service))
                    {
                        logger.Error("could not change service data");
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                        
                }
                logger.Info($"A service was added or changed by user {userId}");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                //write log
                logger.Error($"An error was encountered while adding a service|user_id: {userId}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [AuthFilter]
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
                {
                    logger.Fatal($"user {userId} was authorized, but not found");
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                    

                //getting data about social service for user with userId
                AuthService service = db.FindServiceByUserId(userId);

                if (service == null || String.IsNullOrEmpty(service.AuthId) || String.IsNullOrEmpty(service.AuthToken))
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                //searching friends in vk.com
                VKFriends friendsClient = new VKFriends();
                VKFriendsResponseModel friendsResponse = friendsClient.Search(service.AuthId, service.AuthToken, query);

                if (friendsResponse == null)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);

                if (friendsResponse.Response == null || friendsResponse.Response.Items == null || friendsResponse.Response.Items.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.OK);

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
                                Availability = true,
                                PhotoUri = vkuser.SmallPhotoUri
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
                                Availability = false,
                                PhotoUri = vkuser.SmallPhotoUri
                            });
                        }
                    }
                    catch(Exception e)
                    {
                        logger.Fatal($"couldn't add vkuser to response list", e.Message);
                        continue;
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, userResponseList);

            }
            catch (Exception e)
            {
                logger.Error($"An error was encountered while searching|user_id: {userId}", e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This method check for availability of vk account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true or false</returns>
        [AuthFilter]
        [HttpGet]
        [Route("api/{userId}/services/vk/check")]
        public HttpResponseMessage Check(int userId)
        {
            try
            { 
                //getting data about social service for user with userId
                AuthService service = db.FindServiceByUserId(userId);

                if (service == null || String.IsNullOrEmpty(service.AuthId) || String.IsNullOrEmpty(service.AuthToken))
                    return Request.CreateResponse(HttpStatusCode.OK, "false");

                return Request.CreateResponse(HttpStatusCode.OK, "true");
            }
            catch(Exception e)
            {
                logger.Error($"An error was encountered while checking social service|user_id: {userId}", e.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [AuthFilter]
        [HttpGet]
        [Route("api/{userId}/services/email/sendinvent")]
        public HttpResponseMessage SendInvention(int userId, [FromUri]String email, [FromUri]String signup_url)
        {
            try
            {
                StreamReader messageTemplateFile = (StreamReader)MessageBuilder.GetMessageTemplate("emailTemplate");

                MessageBuilderContext messageContext = new MessageBuilderContext()
                {
                    TemplateFile = messageTemplateFile,
                    SenderName = RequestContext.Principal.Identity.Name,
                    Refer = signup_url
                };
                MessageBuilder messageBuilder = new MessageBuilder(messageContext);

                EmailSender.SendMessageAsync(email, messageBuilder.GetMessage());

            }
            catch(FileNotFoundException e)
            {
                logger.Fatal("email template file wasn't found", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                logger.Warn($"An error was encountered while sending invitation by user|user_id: {userId}", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            logger.Info($"Send invitation by user {userId}");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
