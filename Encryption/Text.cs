using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption
{
    public class MonsieurInference<T> where T :ICipher<IComparable>
    {

    }

    public class Text<T, U> where T : ICipher<U>
    {
        private char[] plainText;
        private char[] cipherText;

        private T cipher;

        private U key;

        public Text(T newCipher, U newKey)
        {
            plainText = null;
            cipherText = null;

            cipher = newCipher;
            key = newKey;
        }

        public string PlainText
        {
            get
            {
                return new string(plainText);
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

        public U Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public void Encrypt()
        {
            cipherText = cipher.Encrypt(plainText, key);
        }

        public void Decrypt()
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

    public class Encryptor<T, U> :IDisposable where T : ICipher<U>
    {
        private Text<T, U> textObject;
        
        private bool encrypted;

        public Encryptor(T newCipher, U newKey)
        {
            textObject = new Text<T, U>(newCipher, newKey);
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

        public U Key
        {
            get
            {
                return textObject.Key;
            }
            set
            {
                textObject.Key = value;
            }
        }

        public void Encrypt()
        {
            if (null != textObject.PlainText)
            {
                textObject.Encrypt();

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

        public void Dispose()
        {

        }
    }

    public class Decryptor<T, U> :IDisposable where T : ICipher<U>
    {
        private Text<T, U> textObject;

        private bool decrypted;

        public Decryptor(T newCipher, U newKey)
        {
            textObject = new Text<T, U>(newCipher, newKey);
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

        public U Key
        {
            get
            {
                return textObject.Key;
            }
            set
            {
                textObject.Key = value;
            }
        }

        public void Decrypt()
        {
            if (null != textObject.CipherText)
            {
                textObject.Decrypt();
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

        public void Dispose()
        {

        }
    }

    public static class Text
    {
        public static Text<T, U> New<T, U>(T newCipher, U newKey) where T : ICipher<U>
        {
            return new Text<T, U>(newCipher, newKey);
        }
    }

    public static class Encryptor
    {
        public static Encryptor<T, U> New<T, U>(T newCipher, U newKey) where T : ICipher<U>
        {
            return new Encryptor<T, U>(newCipher, newKey);
        }
    }

    public static class Decryptor
    {
        public static Decryptor<T, U> New<T, U>(T newCipher, U newKey) where T : ICipher<U>
        {
            return new Decryptor<T, U>(newCipher, newKey);
        }
    }
}
