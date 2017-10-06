using System;
using System.Web.Http;
using SocialServicesLibrary.VkApi;
using System.Configuration;
using NLog;

namespace safemooneyBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        Logger logger = null;
       
        protected void Application_Start()
        {
            logger = LogManager.GetCurrentClassLogger();

            try
            {
                if(logger != null)
                    logger.Info("Application Start");
                //configurate of Routes
                GlobalConfiguration.Configure(WebApiConfig.Register);
                
                //configurate of VKAuthorization
                var authConfigurator = (AuthorizeConfigurator)ConfigurationManager.GetSection("athorizeConfigurator");
                VKAuthorization.Configure(authConfigurator);
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.Error("Application start error");
            }
            
        }
    }
    
}
