using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Security.Principal;
using DataAccessLibrary;
using SharedResourcesLibrary.AccountResources;

namespace safemooneyBackend.Security.Filters
{
    public class AuthFilterAttribute : Attribute, IAuthenticationFilter
    {
        private DataStorageEmulator db = new DataStorageEmulator();

        //Position of username in http request string
        private int positionOfun = 2;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            AuthenticationHeaderValue authentication = context.Request.Headers.Authorization;
            
            if (authentication != null && authentication.Scheme == "Basic")
            {

                int userId = 0;
                try
                {
                    userId = Convert.ToInt32(context.Request.RequestUri.Segments[positionOfun].TrimEnd('/'));
                }
                catch
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[] {
                    new AuthenticationHeaderValue("Basic") }, context.Request);

                    return Task.FromResult<object>(null);
                }

                var d = context.Request.Content;

                string token = authentication.Parameter;
                //check database for this user
                User user = db.FindUserById(userId);

                if(user != null && user.TokenKey != null && user.TokenKey.Equals(token))
                {
                    context.Principal = new GenericPrincipal(new GenericIdentity(user.Login), null);

                    return Task.FromResult<object>(null);
                }

            }

            context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[] {
                    new AuthenticationHeaderValue("Basic") }, context.Request);

            return Task.FromResult<object>(null);
        }
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}