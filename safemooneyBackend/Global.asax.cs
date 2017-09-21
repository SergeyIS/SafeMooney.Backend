using System;
using System.Web.Http;
using SocialServicesLibrary.VkApi;
using System.Configuration;

namespace safemooneyBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                //configurate of Routes
                GlobalConfiguration.Configure(WebApiConfig.Register);
                
                //configurate of VKAuthorization
                var authConfigurator = (AuthorizeConfigurator)ConfigurationManager.GetSection("athorizeConfigurator");
                VKAuthorization.Configure(authConfigurator);
            }
            catch(Exception e)
            {
                //todo: writelog(e)
            }
            
        }
    }
}
