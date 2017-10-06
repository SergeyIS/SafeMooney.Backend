using System;

namespace safemooneyBackend.Models
{
    public class UserResponseModel
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}