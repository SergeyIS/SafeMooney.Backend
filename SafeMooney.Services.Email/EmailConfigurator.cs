using System;
using System.Configuration;

namespace SafeMooney.Services.Email
{
    public class EmailConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("smtpServer", IsRequired = true)]
        public String SmtpServer
        {
            get { return (String)base["smtpServer"]; }
            set { value = (String)base["smtpServer"]; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public Int32 Port
        {
            get { return (Int32)base["port"]; }
            set { value = (Int32)base["port"]; }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public String Username
        {
            get { return (String)base["username"]; }
            set { value = (String)base["username"]; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public String Password
        {
            get { return (String)base["password"]; }
            set { value = (String)base["password"]; }
        }

        [ConfigurationProperty("from", IsRequired = true)]
        public String From
        {
            get { return (String)base["from"]; }
            set { value = (String)base["from"]; }
        }

        [ConfigurationProperty("alias", IsRequired = true)]
        public String Alias
        {
            get { return (String)base["alias"]; }
            set { value = (String)base["alias"]; }
        }

    }
}
