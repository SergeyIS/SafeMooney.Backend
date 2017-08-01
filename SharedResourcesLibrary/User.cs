using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResourcesLibrary
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String TokenKey { get; set; }


        public bool Equals(User other)
        {
            if (other == null)
                throw new ArgumentNullException("Null argement");

            if (other.Login == null)
                throw new ArgumentException("Bad argument");

            return other.Login.Equals(this.Login);
        }
    }
}
