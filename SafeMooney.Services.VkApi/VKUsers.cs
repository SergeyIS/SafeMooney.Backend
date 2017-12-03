﻿using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using SafeMooney.Services.VkApi.Models;

namespace SafeMooney.Services.VkApi
{
    public class VKUsers
    {
        private String _accessToken;

        public VKUsers(String accessToken)
        {
            if (accessToken == null)
                throw new ArgumentNullException("_accessToken is NULL");

            _accessToken = accessToken;
        }


        public VKUserModel GetUserData(String userId)
        {
            if (_accessToken == null)
                throw new Exception("_accessToken is NULL");

            String url = $"https://api.vk.com/method/users.get?user_ids={userId}&access_token={_accessToken}&v=5.68";
            //REVIEW: WebRequest и WebResponse реализуют IDisposable ;)
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(VKUsersResponseModel));
                    VKUsersResponseModel vkUserList = (VKUsersResponseModel)jsonFormatter.ReadObject(stream);

                    if (vkUserList == null || vkUserList.ResponseList == null || vkUserList.ResponseList.Count == 0)
                        return null;

                    return vkUserList.ResponseList[0];
                }
            }
            catch (Exception e)
            {
                //REVIEW: В лог бы что-нибудь черкануть
                return null;
            }
        }
    }
}
