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

        /*
          Example of input: (data must be send in the context)
          {
            "username": "value",
            "password": "value"
          }
         */
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
            response.Username = user.Username;
            response.Access_Token = token;

            return Request.CreateResponse<TokenResponseModel>(HttpStatusCode.OK, response);
        }

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