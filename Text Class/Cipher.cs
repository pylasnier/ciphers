using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    static public partial class Cipher { }

    public interface ICipher<T>
    {
        Type KeyType { get; }

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
