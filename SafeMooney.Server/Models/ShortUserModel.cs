using System;
using System.Text.RegularExpressions;

namespace SafeMooney.Server.Models
{
    public class ShortUserModel : IValidatedModel
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool Availability { get; set; }
        public String AuthorizationId { get; set; }
        public String PhotoUri { get; set; }

        public bool IsValid()
        {
            if (UserId < 0 || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) ||
                FirstName.Length > 20 || LastName.Length > 20 || Username.Length > 20)
                return false;

            Regex regx = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            if (!regx.IsMatch(Username))
                return false;

            return true;
        }
    }
}
