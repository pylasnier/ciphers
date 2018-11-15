using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption.Text
{
    //Contains plain text data, cipher text data, and ecryption methods provided by generic cipher
    public class Text
    {
        private char[] plainText;
        private ICipher cipher;

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

        public string PlainTextFilePath { get; set; }

        public string CipherTextFilePath { get; set; }

        public List<SubKey> KeyStructure
        {
            get
            {
                return cipher.KeyStructure;
            }
        }

        public Text(string assemblyName, string cipherName)
        {
            PlainText = null;
            CipherText = null;

            PlainTextFilePath = null;
            CipherTextFilePath = null;

            cipher = (ICipher) Activator.CreateComInstanceFrom(assemblyName, cipherName).Unwrap();
        }

        public void Encrypt(List<dynamic> key)
        {
            CipherText = cipher.Encrypt(plainText, key);
        }

        public void Decrypt(List<dynamic> key)
        {
            plainText = cipher.Decrypt(CipherText, key);
        }

        public void ReadCipherText()
        {
            using (StreamReader reader = File.OpenText(CipherTextFilePath))
            {
                CipherText = reader.ReadToEnd().ToArray();
            }
        }

        public void WriteCipherText()
        {
            if (false == File.Exists(CipherTextFilePath))
            {
                using (StreamWriter writer = File.CreateText(CipherTextFilePath))
                {
                    writer.Write(CipherText);
                }
            }
        }

        public void ReadPlainText()
        {
            using (StreamReader reader = File.OpenText(PlainTextFilePath))
            {
                CipherText = reader.ReadToEnd().ToArray();
            }
        }

        public void WritePlainText()
        {
            if (false == File.Exists(CipherTextFilePath))
            {
                using (StreamWriter writer = File.CreateText(PlainTextFilePath))
                {
                    writer.Write(CipherText);
                }
            }
        }
    }
    
    //Text class wrapper for just encryption
    public class Encryptor : IDisposable
    {
        private Text textObject;
        
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

        public string PlainTextFilePath
        {
            get
            {
                return textObject.PlainTextFilePath;
            }
            set
            {
                textObject.PlainTextFilePath = value;
            }
        }

        public string CipherTextFilePath
        {
            get
            {
                return textObject.CipherTextFilePath;
            }
            set
            {
                textObject.CipherTextFilePath = value;
            }
        }

        public List<SubKey> KeyStructure
        {
            get
            {
                return textObject.KeyStructure;
            }
        }

        public Encryptor(string assemblyName, string cipherName)
        {
            textObject = new Text(assemblyName, cipherName);
            encrypted = false;
            saved = false;
        }

        public void Encrypt(List<dynamic> key)
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
    public class Decryptor : IDisposable
    {
        private Text textObject;

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

        public List<SubKey> KeyStructure
        {
            get
            {
                return textObject.KeyStructure;
            }
        }

        public Decryptor(string assemblyName, string cipherName)
        {
            textObject = new Text(assemblyName, cipherName);
            decrypted = false;
        }

        public void Decrypt(List<dynamic> key)
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

        public void Dispose() { }
    }
}
