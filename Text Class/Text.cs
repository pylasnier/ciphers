using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ciphers;

namespace Encryption
{
    public class Text<T> where T : Cipher
    {
        private string plainText;
        private string cipherText;

        bool encrypted;

        private T cipher;

        public string PlainText
        {
            get
            {
                return plainText;
            }
        }

        public Text()
        {
            plainText = null;
            cipherText = null;

            encrypted = false;
        }

        public Text(string plainTextInput)
        {
            plainText = plainTextInput;
            cipherText = null;

            encrypted = false;
        }

        public void Encrypt()
        {
            cipherText = T.Encrypt(plainText)
        }
    }
}
