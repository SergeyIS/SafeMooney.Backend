using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SocialServicesLibrary.VkApi.Models
{
    [DataContract]
    public class VKFriendsResponseModel
    {
        [DataMember(Name = "response")]
        public VKFriendListModel Response { get; set; }
    }
}
