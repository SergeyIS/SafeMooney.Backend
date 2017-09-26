using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using safemooneyBackend.Models;
using safemooneyBackend.Security.Util;
using safemooneyBackend.Infrastructure.CustomControllers;
using safemooneyBackend.Security.Filters;
using DataAccessLibrary;
using SharedResourcesLibrary;
using NLog;

namespace safemooneyBackend.Controllers
{
    public class AccountController : ApiController
    {
        private Logger logger = null;
        private IDataAccess db = null;

        public AccountController()
        {
            //todo: dependency injection
            logger = LogManager.GetCurrentClassLogger();
            db = new DataBuilder();
        }

        /// <summary>
         /// This method provide access to resources  for user
         /// </summary>
         /// <param name="user">
         /// {
         ///  "username": "value",
         ///  "password": "value"
         /// }
         /// </param>
         /// <returns></returns>
        [HttpPost]
        [Route("api/account/login")]
        public HttpResponseMessage LogIn(UserRequestModel user)
        {
            if (user == null || user.Username == null || user.Password == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var localUser = db.FindUserByLogin(user.Username);
            //todo: password decryption

            if (localUser == null || !localUser.Password.Equals(user.Password))
            {
                //write log
                logger.Info($"User <username: {user.Username}, password: {user.Password}> is unauthorized");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
                

            
            TokenGenerator tgen = new TokenGenerator(user.Username, user.Password);
            string token = tgen.GenerateKey();

            //save changes to db
            bool resultOfOperation = db.SetTokenForUser(localUser.Id, token);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                     
            TokenResponseModel response = new TokenResponseModel();
            response.UserId = localUser.Id;
            response.Username = localUser.Username;
            response.FirstName = localUser.FirstName;
            response.LastName = localUser.LastName;
            response.Access_Token = token;

            //write log
            logger.Info($"User <username: {user.Username}> is authorized");

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [AuthFilter]
        [HttpGet]
        [Route("api/{userId}/account/logout")]
        public HttpResponseMessage LogOut(int userId = -1)
        {
            bool resultOfOperation = db.ResetTokenForUser(userId);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            //write log
            logger.Info($"User <id: {userId}> logout");

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        /// <summary>
        /// This method register user in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/account/signup")]
        public HttpResponseMessage SignUp(UserRequestModel user)
        {
            if (user == null || user.Username == null || user.Password == null || 
                user.Username == "" || user.Password == "")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (db.CheckForUser(user.Username))
                {
                    //write log
                    logger.Info($"an attempt to create user's account that's already exist <username: {user.Username}>");
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
                    

                db.AddUserSafely(user.Username, user.Password, user.FirstName, user.LastName);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            //write log
            logger.Info($"the user's account was created <username: {user.Username}, id: {user.UserId}>");
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AuthFilter]
        [HttpPost]
        [Route("api/{userId}/account/changeuserinfo")]
        public HttpResponseMessage ChangeUserInfo([FromBody]UserRequestModel user, int userId)
        {
            if (user == null || String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName) ||
                String.IsNullOrEmpty(user.Username))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            User localUser = db.FindUserById(userId);

            if(localUser == null)
            {
                logger.Fatal($"user was authorized, but not found <id: {userId}>");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }



            User changedUser = new User()
            {
                Id = localUser.Id,
                TokenKey = localUser.TokenKey,
                Password = localUser.Password,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            

            bool resultOfOperation = db.ChangeUserInfo(changedUser);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            UserResponseModel resp = new UserResponseModel() { UserId = changedUser.Id, FirstName = changedUser.FirstName, LastName = changedUser.LastName, Username = changedUser.Username };

            //write log
            logger.Info($"user's account was changed from <id: {localUser.Id}, firstname: {localUser.FirstName}, lastname: {localUser.LastName}, username: {localUser.Username}> to <id: {changedUser.Id}, firstname: {changedUser.FirstName}, lastname: {changedUser.LastName}, username: {changedUser.Username}>");

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [AuthFilter]
        [HttpPost]
        [Route("api/{userId}/account/changepass")]
        public HttpResponseMessage ChangePass([FromBody]ChangePasswordRequestModel userCredential, int userId)
        {
            if (userCredential == null || String.IsNullOrEmpty(userCredential.OldPassword) || String.IsNullOrEmpty(userCredential.NewPassword))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            User localUser = db.FindUserById(userId);

            if (localUser == null)
            {
                logger.Fatal($"user was authorized, but not found <id: {userId}>");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            if(localUser.Password != userCredential.OldPassword)
            {
                logger.Info($"user was unauthorized <id: {userId}>");
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            //change password
            localUser.Password = userCredential.NewPassword;
            bool resultOfOperation = db.ChangeUserInfo(localUser);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// This method retern image for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/{userId}/getimg")]
        public IHttpActionResult GetImg(int userId)
        {
            byte[] img = db.GetImage(userId);
            if (img == null)
                return new NotFoundResult(this);

            MemoryStream ms = new MemoryStream(db.GetImage(userId));
            return new FileApiResult(ms, $"{userId}.jpg");
        }

        [AuthFilter]
        [HttpPost]
        [Route("api/{userId}/setimg")]
        public HttpResponseMessage SetImg(int userId)
        {
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            HttpPostedFile file = httpRequest.Files.Get(0);
            

            byte[] bytes = new byte[file.InputStream.Length];
            file.InputStream.Read(bytes, 0, bytes.Length);
            bool res = db.SetImage(userId, bytes);

            if(!res)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}