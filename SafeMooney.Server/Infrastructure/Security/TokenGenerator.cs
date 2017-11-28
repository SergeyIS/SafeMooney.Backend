using System;
using System.Text;
using SafeMooney.Server.Security.Util.Cryptography;

namespace SafeMooney.Server.Security.Util
{
    public class TokenGenerator
    {
        private static String _secretKey = "secret@key";
        private String _username;
        private String _password;
        private String _outstr;

        public TokenGenerator(String username, String password)
        {
            if (username == null || password == null)
                throw new ArgumentNullException("It's not allowed to use this method without setting username and password parameters");

            this._username = username;
            this._password = password;

            _outstr = $"{username}@{password}";
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
                _outstr += rnd.Next(0, 256);
        }

        public String GenerateKey()
        {
            if (_username == null || _password == null)
                throw new ArgumentNullException("It's not allowed to use this method without setting _username and _password parameters");

            CryptoService cryptoService = new CryptoService(new SHA1CryptoAlgorithm());
            
            return cryptoService.ComputeHash(this.ToString());
        }
       
        public override string ToString()
        {
            return _outstr;
        }
    }
}