using System;

namespace SafeMooney.Server.Security.Util.Cryptography
{
    public class CryptoService
    {
        private ICryptoAlgorithm _algorithm;

        public CryptoService(ICryptoAlgorithm algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException();

            _algorithm = algorithm;
        }
        public String ComputeHash(String value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException();

            return _algorithm.GetHashString(value);
        }

        public String Encrypt(String value, String key)
        {
            throw new NotImplementedException();
        }
    }
}
