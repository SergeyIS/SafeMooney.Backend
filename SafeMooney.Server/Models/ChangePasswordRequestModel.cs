using System;
using System.Text.RegularExpressions;

namespace SafeMooney.Server.Models
{
    public class ChangePasswordRequestModel : IValidatedModel
    {
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(OldPassword) || String.IsNullOrEmpty(NewPassword) || OldPassword.Length > 25 || NewPassword.Length > 25)
                return false;

            Regex regx = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            if (!regx.IsMatch(NewPassword))
                return false;

            return true;
        }
    }
}
