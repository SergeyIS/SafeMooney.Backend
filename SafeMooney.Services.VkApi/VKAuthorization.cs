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
           
            String urlString = $"{_configurator.AuthorizationURI}?client_id={_configurator.ClientId}&client_secret={_configurator.ClientSecret}&redirect_uri={_configurator.RedirectUri}&code={code}";
            Stream outstr = null;

            try
            {
                using (outstr = WebClient.ConnectTo(new Uri(urlString)))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(VKAuthorizationResponseModel));
                    VKAuthorizationResponseModel authorizationResponse = (VKAuthorizationResponseModel)jsonFormatter.ReadObject(outstr);
                    return authorizationResponse;
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (outstr != null)
                        outstr.Close();
                }
                catch
                {
                    throw new Exception("Cannot close output stream", e);
                }

                throw;
            }
        }
    }
}
