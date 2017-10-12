using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SocialServicesLibrary.Email
{
    public class EmailSender
    {
        private static EmailConfigurator _configurator;
        public static void Configure(EmailConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator is NULL");

            _configurator = configurator;
        }

        public static void SendMessageAsync(String email, String message)
        {
            if(_configurator == null || String.IsNullOrEmpty(_configurator.SmtpServer) || String.IsNullOrEmpty(_configurator.Username) || 
                String.IsNullOrEmpty(_configurator.Password) || String.IsNullOrEmpty(_configurator.From))
            {
                throw new Exception("Bad configuration");
            }

            Task.Run(() => {
                try
                {
                    MailMessage _message = new MailMessage(new MailAddress(_configurator.From, _configurator.Alias), new MailAddress(email))
                    {
                        Body = message,
                        IsBodyHtml = true
                    };

                    using (SmtpClient smtp = new SmtpClient(_configurator.SmtpServer, _configurator.Port))
                    {
                        smtp.Credentials = new NetworkCredential(_configurator.Username, _configurator.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(_message);
                    }
                }
                catch (Exception e)
                {
                    //todo: write log
                }
            });  
        }
    }
}
