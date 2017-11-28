using System;

namespace SafeMooney.Server.Security.Util.Cryptography
{
    public interface ICryptoAlgorithm
    {
        String GetHashString(String value);
    }
}
