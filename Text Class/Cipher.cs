using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class Cipher : ICipher<object>
    {
        public char[] Encrypt(string plainText, object key)
        {
            return null;
        }
    }

    public interface ICipher<T>
    {
        char[] Encrypt(string plainText, T key);
    }

    /**
    public class Cipher : ICipher<Cipher.Key>
    {
        public char[] Encrypt(string plainText, Key key)
        {
            return null;
        }

        public class Key { }
    }
    **/
}
