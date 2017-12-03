using System.Security.Cryptography;
using System.Text;

namespace SafeMooney.Server.Security.Util.Cryptography
{
    //REVIEW: А чем стандартный не угодил?

    public class SHA1CryptoAlgorithm : ICryptoAlgorithm
    {
        public string GetHashString(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            SHA1CryptoServiceProvider cipher = new SHA1CryptoServiceProvider();
            byte[] byteHash = cipher.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
