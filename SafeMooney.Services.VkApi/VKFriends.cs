using System;
using SafeMooney.Services.VkApi.Models;
using System.Net;
using System.Runtime.Serialization.Json;
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

            String url = $"https://api.vk.com/method/friends.search?user_id={authId}&q={query}&fields=photo_50&access_token={accessToken}&v=5.68";
            //REVIEW: WebRequest и WebResponse реализуют IDisposable
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(VKFriendsResponseModel));

                    VKFriendsResponseModel friendsResponse = (VKFriendsResponseModel)DeserializeFromStream<VKFriendsResponseModel>(stream);
                    return friendsResponse;
                }
            }
            catch (Exception e)
            {
                //REVIEW: лог? 
                return null;
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
