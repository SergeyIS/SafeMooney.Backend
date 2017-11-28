using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Security.Principal;
using SafeMooney.Shared;
using SafeMooney.Shared.Models;
using System.Web.Mvc;
using NLog;

namespace SafeMooney.Server.Security.Filters
{
    public class AuthFilterAttribute : Attribute, IAuthenticationFilter
    {
        private IDataStorage _db = null;
        private Logger _logger = null;

        public AuthFilterAttribute()
        {
            _db = DependencyResolver.Current.GetService<IDataStorage>();
            _logger = LogManager.GetCurrentClassLogger();
        }

        //Position of _username in http request string
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
                try
                {
                    User user = _db.FindUserById(userId);

                    if (user != null && user.TokenKey != null && user.TokenKey.Equals(token))
                    {
                        context.Principal = new GenericPrincipal(new GenericIdentity(user.Username), null);

                        return Task.FromResult<object>(null);
                    }
                }
                catch(Exception e)
                {
                    //write log
                    _logger.Error($"An error was occured while authorization proccess", e);
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