using System.Security.Cryptography;
using System.Text;

namespace SafeMooney.Server.Security.Util.Cryptography
{
    //REVIEW: Выжечь напалмом. MD5 не нужен
    public class MD5CryptoAlgorithm : ICryptoAlgorithm
    {
        public string GetHashString(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            MD5CryptoServiceProvider cipher = new MD5CryptoServiceProvider();
            byte[] byteHash = cipher.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
