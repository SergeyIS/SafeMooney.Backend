using System;

namespace SafeMooney.Server.Models
{
    public class TokenResponseModel
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Access_Token { get; set; }
    }
}
