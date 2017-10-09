using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SocialServicesLibrary.VkApi.Models
{
    [DataContract]
    public class VKFriendListModel
    {
        [DataMember(Name = "items")]
        public List<VKUserModel> Items { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
