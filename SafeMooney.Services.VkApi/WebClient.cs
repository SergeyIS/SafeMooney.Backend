using System;
using System.IO;
using System.Net;

namespace SafeMooney.Services.VkApi
{
    static class WebClient
    {
        public static Stream ConnectTo(Uri url)
        {
            Stream result = null;
            WebRequest request = null;
            WebResponse response = null;

            try
            {
                request = WebRequest.Create(url);
                response = request.GetResponse();
                result = new BufferedStream(response.GetResponseStream());

                if (response != null)
                    response.Close();

                return result;
            }
            catch(Exception e)
            {
                try
                {
                    if (result != null)
                        result.Close();

                    request = null; response = null; result = null;
                }
                catch
                {
                    throw new Exception("Cannot close BufferedStream", e);
                }

                throw;
            }
        }
    }
}
