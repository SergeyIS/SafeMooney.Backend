using System;
using System.Runtime.Serialization;

namespace SafeMooney.Services.VkApi.Models
{
    [DataContract]
    public class VKAuthorizationResponseModel
    {
        [DataMember(Name = "access_token")]
        public String AccessToken { get; set; }

        [DataMember(Name = "user_id")]
        public String UserId { get; set; }

        [DataMember(Name = "expires_in")]
        public String ExpiresIn { get; set; }
    }
}