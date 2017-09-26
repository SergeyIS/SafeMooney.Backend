using System;

namespace safemooneyBackend.Models
{
    public class ChangePasswordRequestModel
    {
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
    }
}