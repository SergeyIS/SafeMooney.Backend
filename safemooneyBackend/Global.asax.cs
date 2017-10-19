using System;
using System.Web.Http;
using SocialServicesLibrary.VkApi;
using System.Configuration;
using NLog;
using SocialServicesLibrary.Email;
using System.IO;

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
                
                //configurate of social services
                var authConfigurator = (AuthorizeConfigurator)ConfigurationManager.GetSection("athorizeConfigurator");
                VKAuthorization.Configure(authConfigurator);
                var emailConfigurator = (EmailConfigurator)ConfigurationManager.GetSection("emailConfigurator");
                EmailSender.Configure(emailConfigurator);

                //set template file in cache
                String path = String.Empty;
                try
                {
                    path = new String(ConfigurationManager.AppSettings.Get("tmpl_path").ToCharArray());
                }
                catch(Exception e)
                {
                    //todo: write log
                    path = "default";
                }

                if (path.Equals("default", StringComparison.OrdinalIgnoreCase))
                    path = $"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}\\default.vm";//default template file name

                MessageBuilder.SetMessageTemplate("emailTemplate", new StreamReader(path));
            }
            catch(Exception e)
            {
                if (logger != null)
                    logger.Fatal("Application start error", e.Message);

            }
            
        }
    }
    
}
