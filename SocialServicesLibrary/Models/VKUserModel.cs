using System;
using System.Runtime.Serialization;

namespace SocialServicesLibrary.VkApi.Models
{
    [DataContract]
    public class VKUserModel
    {
        [DataMember(Name = "id")]
        public String UserId { get; set; }

        [DataMember(Name = "first_name")]
        public String FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public String LastName { get; set; }

        [DataMember(Name = "email")]
        public String Email { get; set; }
    }
}
