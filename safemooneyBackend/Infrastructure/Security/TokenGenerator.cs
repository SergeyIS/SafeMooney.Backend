using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace safemooneyBackend.Security.Util
{
    public class TokenGenerator
    {
        private const String secretKey = "secret@key";
        private String username;
        private String password;
        private String outstr;

        public TokenGenerator(String username, String password)
        {
            this.username = username;
            this.password = password;

            outstr = $"{username}@{password}";
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
                outstr += rnd.Next(0, 256);
        }

        public String GenerateKey()
        {
            if (username == null || password == null)
                throw new Exception("It's not allowed to use this method without setting username and password parameters");

            byte[] key = Encoding.Unicode.GetBytes(secretKey);
            byte[] data = Encoding.Unicode.GetBytes(this.ToString());

            CryptoService cryptoService = new CryptoService();
            byte[] cipherData = cryptoService.Encrypt(data, key);
            string cipherPlainText = "";
            foreach (var b in cipherData)
            {
                cipherPlainText += b.ToString("x2");
            }

            return cipherPlainText;
        }
       
        public override string ToString()
        {
            return outstr;
        }
    }
}