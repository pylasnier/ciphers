using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class Text<T, U> where T : ICipher<U>
    {
        private char[] plainText;
        private char[] cipherText;

        private T cipher;

        bool encrypted;
        bool decrypted;

        public string PlainText
        {
            get
            {
                return plainText.ToString();
            }
        }

        public Text(T newCipher)
        {
            plainText = null;
            cipherText = null;
            
            cipher = newCipher;

            decrypted = false;
            encrypted = false;
        }

        public Text(T newCipher, string plainTextInput)
        {
            plainText = plainTextInput.ToArray();
            cipherText = null;

            if (newCipher.KeyType != typeof(U))
            {
                throw new ArgumentException();
            }
            cipher = newCipher;

            decrypted = true;
            encrypted = false;
        }

        public void Encrypt(U key)
        {
            encrypted = true;
            cipherText = cipher.Encrypt(plainText, key);
        }

        public void SetPlainText(string newPlainText)
        {
            plainText = newPlainText.ToArray();
        }

        public char[] GetCipherText()
        {
            return cipherText;
        }
    }
}
