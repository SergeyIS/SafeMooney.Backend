using System;
using System.IO;
using System.Web.Http;
using System.Configuration;
using SafeMooney.Services.Email;
using SafeMooney.Services.VkApi;
using NLog;
using System.Diagnostics;
using Ninject;
using System.Web.Mvc;
using Ninject.Web.Mvc;

namespace SafeMooney.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Logger _logger = null;
       
        protected void Application_Start()
        {
            try
            {
                //global exception handler registration
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalExceptionHandler);

                _logger = LogManager.GetCurrentClassLogger();
                if (_logger != null)
                    _logger.Info("Application Start");
                
                //configurate of Routes and IoC container
                GlobalConfiguration.Configure(WebApiConfig.Register);

                //configurate of social services
                var authConfigurator = (AuthorizeConfiguration)ConfigurationManager.GetSection("athorizeConfiguration");
                VKAuthorization.Configure(authConfigurator);
                var emailConfigurator = (EmailConfiguration)ConfigurationManager.GetSection("emailConfiguration");
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
                if (_logger != null)
                    _logger.Fatal("Application start error", e.Message);

            }
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception exception = (Exception) args.ExceptionObject;
                LogManager.GetCurrentClassLogger().Fatal(exception, $"Fatall error was occured: {exception.Message}");
            }
            catch(Exception e)
            {
                EventLog systemLogger = new EventLog();
                systemLogger.WriteEntry($"Fatall error was occured: {e.Message}");
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        ~WebApiApplication()
        {
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(GlobalExceptionHandler);
        }

    }
}
