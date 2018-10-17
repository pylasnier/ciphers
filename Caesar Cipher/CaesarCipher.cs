using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers
{
    public class CCipher : Cipher
    {
        public char[] Encrypt(string plainText, int key)
        {
            char[] cipherText;
            int i;

            key = Math.Abs(key) % char.MaxValue;
            cipherText = new char[plainText.Length];

            for (i = 0; i < plainText.Length; i++)
            {
                cipherText[i] = (char) (plainText[i] + key);
            }

            return cipherText;
        }
    }
}
