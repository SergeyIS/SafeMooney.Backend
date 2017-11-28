using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using SafeMooney.Services.VkApi.Models;

namespace SafeMooney.Services.VkApi
{
    public class VKAuthorization
    {
        private static AuthorizeConfiguration _configurator;

        public static void Configure(AuthorizeConfiguration configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator can't be NULL");

            _configurator = configurator;
        }

        public VKAuthorizationResponseModel Authorize(String code)
        {
            if (_configurator == null)
                throw new Exception("configuration is not found");

            String url = $"{_configurator.AuthorizationURI}?client_id={_configurator.ClientId}&client_secret={_configurator.ClientSecret}&redirect_uri={_configurator.RedirectUri}&code={code}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(VKAuthorizationResponseModel));
                    VKAuthorizationResponseModel authorizationResponse = (VKAuthorizationResponseModel)jsonFormatter.ReadObject(stream);
                    return authorizationResponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
