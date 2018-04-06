using System;
using System.Text.RegularExpressions;

namespace SafeMooney.Server.Models
{
    public class UserRequestModel : IValidatedModel
    {
        public int UserId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public bool IsValid()
        {
            if (UserId < 0 || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(FirstName) ||
                String.IsNullOrEmpty(LastName) || Username.Length > 20 || Password.Length > 50 || FirstName.Length > 20 || LastName.Length > 20)
                return false;

            Regex regx_username = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            Regex regx_password = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            if (!regx_username.IsMatch(Username) || !regx_password.IsMatch(Password))
                return false;
            
            return true;
        }

        public bool IsValidOnSignIn()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || 
                Username.Length > 20 || Password.Length > 50)
                return false;

            return true;
        }

        public bool IsValideOnSignUp()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(FirstName) ||
                String.IsNullOrEmpty(LastName) || Username.Length > 20 || Password.Length > 50 || FirstName.Length > 20 || LastName.Length > 20)
                return false;

            Regex regx_username = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            Regex regx_password = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            if (!regx_username.IsMatch(Username) || !regx_password.IsMatch(Password))
                return false;

            return true;
        }

        public bool IsValidOnChangeIngo()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(FirstName) ||
                String.IsNullOrEmpty(LastName) || Username.Length > 20 || FirstName.Length > 20 || LastName.Length > 20)
                return false;

            Regex regx_username = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            if (!regx_username.IsMatch(Username))
                return false;

            return true;
        }
    }
}
