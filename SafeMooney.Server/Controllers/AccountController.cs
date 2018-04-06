using System;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using SafeMooney.Server.Models;
using SafeMooney.Server.Security.Util;
using SafeMooney.Server.Infrastructure.CustomControllers;
using SafeMooney.Server.Security.Filters;
using SafeMooney.Shared;
using SafeMooney.Shared.Models;
using SafeMooney.Server.Infrastructure.Dependencies;
using NLog;
using System.Web.Http.Results;

namespace SafeMooney.Server.Controllers
{
    public class AccountController : ApiController
    {
        private Logger _logger = null;
        private IDataStorage _db = null;

        public AccountController()
        {
            _logger = LogManager.GetCurrentClassLogger();

            try
            {   
                _db = (IDataStorage)DependencyContainer.GetService(typeof(IDataStorage));
            }
            catch
            {
                _logger.Error("Cannot get database instance from IoC container");
            }          
        }
        /// <summary>
         /// This method provide access to resources  for user
         /// </summary>
         /// <param name="user">
         /// {
         ///  "_username": "value",
         ///  "_password": "value"
         /// }
         /// </param>
         /// <returns></returns>
        [HttpPost]
        [Route("api/account/login")]
        public HttpResponseMessage LogIn(UserRequestModel user)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            try
            {
                if (user == null || !user.IsValidOnSignIn())
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var localUser = _db.FindUserByLogin(user.Username);
                //todo: _password decryption

                if (localUser == null || !localUser.Password.Equals(user.Password))
                {
                    //write log
                    _logger.Info($"User <_username: {user.Username}, _password: {user.Password}> is unauthorized");
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

                TokenGenerator tgen = new TokenGenerator(user.Username, user.Password);
                string token = tgen.GenerateKey();

                //save changes to _db
                bool resultOfOperation = _db.SetTokenForUser(localUser.Id, token);

                if (!resultOfOperation)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);

                TokenResponseModel response = new TokenResponseModel();
                response.UserId = localUser.Id;
                response.Username = localUser.Username;
                response.FirstName = localUser.FirstName;
                response.LastName = localUser.LastName;
                response.Access_Token = token;

                //write log
                _logger.Info($"User <_username: {user.Username}> is authorized");

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception e)
            {
                _logger.Error($"An error was occured: {e.Message}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [AuthFilter]
        [HttpGet]
        [Route("api/{userId}/account/logout")]
        public HttpResponseMessage LogOut(int userId = -1)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            bool resultOfOperation = default(bool);

            try
            {
                resultOfOperation = _db.ResetTokenForUser(userId);
            }
            catch(Exception e)
            {
                _logger.Error($"Cannot reset token for user {userId}", e);
            }

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            //write log
            _logger.Info($"User <id: {userId}> logout");

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
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (user == null || !user.IsValideOnSignUp())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (_db.CheckForUser(user.Username))
                {
                    //write log
                    _logger.Info($"an attempt to create user's account that's already exist <_username: {user.Username}>");
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
                
                _db.AddUserSafely(user.Username, user.Password, user.FirstName, user.LastName);
            }
            catch(Exception e)
            {
                _logger.Error("Cannot add user account", e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            //write log
            _logger.Info($"the user's account was created <_username: {user.Username}, id: {user.UserId}>");
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AuthFilter]
        [HttpPost]
        [Route("api/{userId}/account/changeuserinfo")]
        public HttpResponseMessage ChangeUserInfo([FromBody]UserRequestModel user, int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (user == null || !user.IsValidOnChangeIngo() || userId < 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            User localUser = _db.FindUserById(userId);

            if(localUser == null)
            {
                _logger.Error($"user was authorized, but not found <id: {userId}>");
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

            try
            {
                _db.ChangeUserInfo(changedUser);
            }
            catch(Exception e)
            {
                _logger.Error($"Cannot change user {userId} info", e);

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }                

            UserResponseModel resp = new UserResponseModel() { UserId = changedUser.Id, FirstName = changedUser.FirstName, LastName = changedUser.LastName, Username = changedUser.Username };

            //write log
            _logger.Info($"user's account was changed from <id: {localUser.Id}, firstname: {localUser.FirstName}, lastname: {localUser.LastName}, _username: {localUser.Username}> to <id: {changedUser.Id}, firstname: {changedUser.FirstName}, lastname: {changedUser.LastName}, _username: {changedUser.Username}>");

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [AuthFilter]
        [HttpPost]
        [Route("api/{userId}/account/changepass")]
        public HttpResponseMessage ChangePass([FromBody]ChangePasswordRequestModel userCredential, int userId)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            if (userCredential == null || !userCredential.IsValid())
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            User localUser = _db.FindUserById(userId);

            if (localUser == null)
            {
                _logger.Fatal($"user was authorized, but not found <id: {userId}>");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            if(localUser.Password != userCredential.OldPassword)
            {
                _logger.Info($"user was unauthorized <id: {userId}>");
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            //change _password
            localUser.Password = userCredential.NewPassword;

            //saving changes
            try
            {
                _db.ChangeUserInfo(localUser);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// This method retern image for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getimg/{filename}")]
        public IHttpActionResult GetImg(String filename)
        {
            if (_db == null)
                return this.InternalServerError();

            String[] filenameSplitted = null;
            int userId = 0;

            if (filename == null || (filenameSplitted = filename.Split('.')).Length != 2 || !Int32.TryParse(filenameSplitted[0], out userId))
                return BadRequest();

            try
            {
                UserImage img = _db.GetImage(userId);
                if (img == null)
                {
                    img = _db.GetImage(0);
                    if (img == null)
                        return NotFound();
                }

                img.Name = filenameSplitted[0];
                img.Format = filenameSplitted[1];

                return new ImageApiResult(img);
            }
            catch(Exception e)
            {
                _logger.Error("Cannot get user {userId} image", e);
                return this.InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/{userId}/setimg")]
        public HttpResponseMessage SetImg(int userId, [FromUri]String filename)
        {
            if (_db == null)
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 0 || String.IsNullOrEmpty(filename))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            HttpPostedFile file = httpRequest.Files.Get(0);
            byte[] bytes = new byte[file.InputStream.Length];
            file.InputStream.Read(bytes, 0, bytes.Length);

            var fileSplitted = filename.Split('.');
            if (filename == null || fileSplitted.Length != 2)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                _db.SetImage(new UserImage() { UserId = userId, Name = fileSplitted[0], Format = fileSplitted[1], Data = bytes });
            }
            catch(Exception e)
            {
                _logger.Error("Cannot set an image for user {userId}", e);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}