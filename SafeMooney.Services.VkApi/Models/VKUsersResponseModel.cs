using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SafeMooney.Services.VkApi.Models
{
    [DataContract]
    public class VKUsersResponseModel
    {
        [DataMember(Name = "response")]
        public List<VKUserModel> ResponseList { get; set; }
    }
}
