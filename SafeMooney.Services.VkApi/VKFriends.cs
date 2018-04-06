using System;
using SafeMooney.Services.VkApi.Models;
using System.IO;
using Newtonsoft.Json;

namespace SafeMooney.Services.VkApi
{
    public class VKFriends
    {
        public VKFriendsResponseModel Search(String authId, String accessToken ,String query)
        {
            if (String.IsNullOrEmpty(authId) || String.IsNullOrEmpty(accessToken) || String.IsNullOrEmpty(query))
                throw new ArgumentNullException();

            String urlString = $"https://api.vk.com/method/friends.search?user_id={authId}&q={query}&fields=photo_50&access_token={accessToken}&v=5.68";
            Stream outstr = null;
            
            try
            {
                using (outstr = WebClient.ConnectTo(new Uri(urlString)))
                {
                    VKFriendsResponseModel friendsResponse = (VKFriendsResponseModel)DeserializeFromStream<VKFriendsResponseModel>(outstr);
                    return friendsResponse;
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

        private static object DeserializeFromStream<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}
