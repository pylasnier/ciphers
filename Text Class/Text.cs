using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class TText<T>
    {
        public TText(T newCipher)
        {

        }
    }

    public class Text<T> where T : Cipher
    {
        private string plainText;
        private char[] cipherText;

        private T cipher;

        bool encrypted;

        public string PlainText
        {
            get
            {
                return plainText;
            }
        }

        public Text(T newCipher)
        {
            plainText = null;
            cipherText = null;

            cipher = newCipher;

            encrypted = false;
        }

        public Text(T newCipher, string plainTextInput)
        {
            plainText = plainTextInput;
            cipherText = null;

            cipher = newCipher;

            encrypted = false;
        }

        public void Encrypt(object key)
        {
            encrypted = true;
            cipherText = cipher.Encrypt(plainText, key);
        }

        public void SetPlainText(string newPlainText)
        {
            plainText = newPlainText;
        }

        public char[] GetCipherText()
        {
            return cipherText;
        }
    }
}
