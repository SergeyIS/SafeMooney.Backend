using System;
using SharedResourcesLibrary;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace SocialServicesLibrary.VkApi
{
    public class VKAuthorization
    {
        private AuthorizeConfigurator _configurator;

        public VKAuthorization(AuthorizeConfigurator configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator can't be NULL");

            _configurator = configurator;
        }
        public OAuthAuthorization Authorize(String code)
        {
            //get parametrs from _configurator
            String app_id = "6190519";
            String secure_code = "0edpRkVRYAZxd5p4zJQG";
            String redirect_url = "http://localhost:50266/api/oauth/authorize";
            String url = $"https://oauth.vk.com/access_token?client_id={app_id}&client_secret={secure_code}&redirect_uri={redirect_url}&code={code}";

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
