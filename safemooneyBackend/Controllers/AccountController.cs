using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using safemooneyBackend.Security.Filters;
using DataAccessLibrary;
using SharedResourcesLibrary.AccountResources;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using safemooneyBackend.Models;
using safemooneyBackend.Security.Util;

namespace safemooneyBackend.Controllers
{
    public class AccountController : ApiController
    {
        private DataStorageEmulator db = new DataStorageEmulator();

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
        [Route("api/account/login/")]
        public HttpResponseMessage LogIn(UserModel user)
        {
            if (user == null || user.Username == null || user.Password == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            User localUser = db.FindUserByLogin(user.Username);
            //todo: password decryption

            if (localUser == null || !localUser.Password.Equals(user.Password))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            
            TokenGenerator tgen = new TokenGenerator(user.Username, user.Password);
            string token = tgen.GenerateKey();

            //save changes to db
            localUser.TokenKey = token;
                        
            TokenResponseModel response = new TokenResponseModel();
            response.UserId = localUser.Id;
            response.Access_Token = token;

            return Request.CreateResponse<TokenResponseModel>(HttpStatusCode.OK, response);
        }

        [AuthFilter]
        [HttpGet]
        [Route("api/{userId}/account/logout")]
        public HttpResponseMessage LogOut(int userId = -1)
        {
            bool resultOfOperation = db.ResetTokenForUser(userId);

            if (!resultOfOperation)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        /// <summary>
        /// This method register user in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/account/signup/")]
        public HttpResponseMessage SignUp(UserModel user)
        {
            if (user == null || user.Username == null || user.Password == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            if (db.CheckForUser(user.Username))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            db.AddUser(user.Username, user.Password, user.FirstName, user.LastName);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}