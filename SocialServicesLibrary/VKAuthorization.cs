using System;
using SharedResourcesLibrary;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace SocialServicesLibrary.VkApi
{
    public class VKAuthorization
    {
        private static AuthorizeConfigurator _configurator;

        public static void Configure(AuthorizeConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator can't be NULL");

            _configurator = configurator;
        }
        public OAuthAuthorization Authorize(String code)
        {
            if (_configurator == null)
                throw new Exception("configuration is not found");

            String url = $"https://oauth.vk.com/access_token?client_id={_configurator.ClientId}&client_secret={_configurator.ClientSecret}&redirect_uri={_configurator.RedirectUri}&code={code}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(OAuthAuthorization));
                    OAuthAuthorization oauthorization = (OAuthAuthorization)jsonFormatter.ReadObject(stream);
                    //search for user

                    return oauthorization;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
