using System.Security.Cryptography;
using System.Text;

namespace SafeMooney.Server.Security.Util.Cryptography
{
    public class SHA256CryptoAlgorithm : ICryptoAlgorithm
    {
        public string GetHashString(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            SHA256CryptoServiceProvider cipher = new SHA256CryptoServiceProvider();
            byte[] byteHash = cipher.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
