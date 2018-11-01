using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption
{
    //Contains plain text data, cipher text data, and ecryption methods provided by generic cipher
    public class Text<T, U> where T : ICipher<U>
    {
        private char[] plainText;
        private T cipher;

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

        public char[] CipherText { get; set; }

        public U Key { get; set; }

        public Text(T newCipher, U newKey)
        {
            plainText = null;
            CipherText = null;

            cipher = newCipher;
            Key = newKey;
        }

        public void Encrypt()
        {
            CipherText = cipher.Encrypt(plainText, Key);
        }

        public void Decrypt()
        {
            plainText = cipher.Decrypt(CipherText, Key);
        }

        public void GetCipherText(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                CipherText = reader.ReadToEnd().ToArray();
            }
        }

        public void SaveCipherText(string path)
        {
            if (false == File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(CipherText);
                }
            }
        }
    }

    //Text class wrapper for just encryption
    public class Encryptor<T, U> :IDisposable where T : ICipher<U>
    {
        private Text<T, U> textObject;
        
        private bool encrypted;
        private bool saved;

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

        public Encryptor(T newCipher, U newKey)
        {
            textObject = new Text<T, U>(newCipher, newKey);
            encrypted = false;
            saved = false;
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
                    saved = true;
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
            if (false == saved && true == encrypted)
            {
                throw new InvalidOperationException("Encryptor disposed before cipher text was saved");
            }
        }
    }

    //Text class wrapper for just decryption
    public class Decryptor<T, U> :IDisposable where T : ICipher<U>
    {
        private Text<T, U> textObject;

        private bool decrypted;

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

        public Decryptor(T newCipher, U newKey)
        {
            textObject = new Text<T, U>(newCipher, newKey);
            decrypted = false;
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

        public void Dispose() { }
    }

    //Constructors for Text class and wrapper classes
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
