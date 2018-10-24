using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption
{
    public enum CipherType
    {
        Uninstantiated = 0,
        Encryptor = 1,
        Decryptor = 2
    };

    public class Text<T, U> where T : ICipher<U>
    {
        private char[] plainText;
        private char[] cipherText;

        private T cipher;

        public Text(T newCipher)
        {
            plainText = null;
            cipherText = null;

            cipher = newCipher;
        }

        public string PlainText
        {
            get
            {
                return plainText.ToString();
            }
            set
            {
                plainText = value.ToArray();
            }
        }

        public char[] CipherText
        {
            get
            {
                return cipherText;
            }
            set
            {
                cipherText = value;
            }
        }

        public void Encrypt(U key)
        {
            cipherText = cipher.Encrypt(plainText, key);
        }

        public void Decrypt(U key)
        {
            plainText = cipher.Decrypt(cipherText, key);
        }

        public void GetCipherText(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                cipherText = reader.ReadToEnd().ToArray();
            }
        }

        public void SaveCipherText(string path)
        {
            if (false == File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(cipherText);
                }
            }
        }
    }

    public class Encryptor<T, U> where T : ICipher<U>
    {
        private Text<T, U> textObject;
        
        private bool encrypted;

        public Encryptor(T newCipher)
        {
            textObject = new Text<T, U>(newCipher);
            encrypted = false;
        }

        public string PlainText
        {
            get
            {
                return textObject.PlainText;
            }
            set
            {
                textObject.PlainText = value;
                encrypted = false;
            }
        }

        public char[] CipherText
        {
            get
            {
                return textObject.CipherText;
            }
        }

        public void Encrypt(U key)
        {
            if (null != textObject.PlainText)
            {
                textObject.Encrypt(key);

                encrypted = true;
            }
            else
            {
                throw new InvalidOperationException("No plain text given");
            }
        }

        public void SaveCipherText(string path)
        {
            if (false == File.Exists(path))
            {
                if (true == encrypted)
                {
                    textObject.SaveCipherText(path);
                }
                else
                {
                    throw new InvalidOperationException("Text hasn't been encrypted");
                }
            }
            else
            {
                throw new InvalidOperationException("File already exists at given path");
            }
        }
    }

    public class Decryptor<T, U> where T : ICipher<U>
    {
        private Text<T, U> textObject;

        private bool decrypted;

        public Decryptor(T newCipher)
        {
            textObject = new Text<T, U>(newCipher);
            decrypted = false;
        }

        public string PlainText
        {
            get
            {
                if (true == decrypted)
                {
                    return textObject.PlainText;
                }
                else
                {
                    throw new InvalidOperationException("Text hasn't been decrypted");
                }
            }
        }

        public char[] CipherText
        {
            get
            {
                return textObject.CipherText;
            }
        }

        public void Decrypt(U key)
        {
            if (null != textObject.CipherText)
            {
                textObject.Decrypt(key);
                decrypted = true;
            }
            else
            {
                throw new InvalidOperationException("No cipher text exists");
            }
        }

        public void GetCipherText(string path)
        {
            if (true == File.Exists(path))
            {
                textObject.GetCipherText(path);
            }
            else
            {
                throw new FileNotFoundException("File doesn't exist");
            }
        }
    }

    public static class Text
    {
        public static Text<T, U> New<T, U>(T newCipher, U ignoreThis = default(U)) where T : ICipher<U>
        {
            return new Text<T, U>(newCipher);
        }
    }
}
