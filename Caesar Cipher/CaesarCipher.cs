using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;

namespace Encryption
{
    public class CCipher : ICipher<int>
    {
        public Type KeyType
        {
            get
            {
                return typeof(int);
            }
        }

        public char[] Encrypt(char[] plainText, int key)
        {
            char[] cipherText;
            int i;

            key = Math.Abs(key) % char.MaxValue;
            cipherText = new char[plainText.Length];

            for (i = 0; i < plainText.Length; i++)
            {
                cipherText[i] = (char)(plainText[i] + key);
            }

            return cipherText;
        }

        public char[] Encrypt(string plainText, int key)
        {
            return Encrypt(plainText.ToArray(), key);
        }

        public char[] Decrypt(char[] cipherText, int key)
        {
            return Encrypt(cipherText, char.MaxValue - key);
        }

        public char[] Decrypt(string cipherText, int key)
        {
            return Encrypt(cipherText, char.MaxValue - key);
        }
    }
}
