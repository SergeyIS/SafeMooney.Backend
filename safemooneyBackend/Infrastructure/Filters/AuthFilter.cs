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

namespace safemooneyBackend.Filters
{
    public class AuthFilter : Attribute, IAuthenticationFilter
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
                string username = context.Request.RequestUri.Segments[positionOfun].TrimEnd('/');
                string token = authentication.Parameter;
                //check data base for this user
                User user = db.FindUserByLogin(username);

                if(user != null && user.TokenKey != null && user.TokenKey.Equals(token))
                {
                    context.Principal = new GenericPrincipal(new GenericIdentity(username), null);

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