using System;
using System.Runtime.Serialization;

namespace SafeMooney.Services.VkApi.Models
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

        [DataMember(Name = "photo_50")]
        public String SmallPhotoUri { get; set; }

        [DataMember(Name = "photo_100")]
        public String MediumPhotoUri { get; set; }

        [DataMember(Name = "photo_200")]
        public String LargePhotoUri { get; set; }
    }
}
