using System;
using System.Configuration;

namespace SafeMooney.Services.VkApi
{
    public class AuthorizeConfigurator : ConfigurationSection 
    {
        [ConfigurationProperty("authorizationUri", IsRequired = true)]
        public String AuthorizationURI
        {
            get { return (String)base["authorizationUri"]; }
            set { value = (String)base["authorizationUri"]; }
        }

        [ConfigurationProperty("clientId", IsRequired = true)]
        public String ClientId
        {
            get { return (String)base["clientId"]; }
            set { value = (String)base["clientId"]; }
        }

        [ConfigurationProperty("clientSecret", IsRequired = true)]
        public String ClientSecret
        {
            get { return (String)base["clientSecret"]; }
            set { value = (String)base["clientSecret"]; }
        }

        [ConfigurationProperty("redirectUri", IsRequired = true)]
        public String RedirectUri
        {
            get { return (String)base["redirectUri"]; }
            set { value = (String)base["redirectUri"]; }
        }
    }
}
