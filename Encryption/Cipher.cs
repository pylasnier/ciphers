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
        char[] Encrypt(char[] plainText, T key);
        char[] Encrypt(string plainText, T key);
        char[] Decrypt(char[] cipherText, T key);
        char[] Decrypt(string cipherText, T key);
    }
}
