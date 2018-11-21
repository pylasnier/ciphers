using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Encryption.Text
{
    //Contains plain text data, cipher text data, and ecryption methods provided by generic cipher
    public class Text : IDisposable
    {
        private ICipher cipher;

        public char[] PlainText { get; set; }

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

        public Text(ICipher newCipher)
        {
            PlainText = null;
            CipherText = null;

            PlainTextFilePath = null;
            CipherTextFilePath = null;

            cipher = newCipher;
        }

        public void Encrypt(List<dynamic> key)
        {
            CipherText = cipher.Encrypt(PlainText, key);
        }

        public void Decrypt(List<dynamic> key)
        {
            PlainText = cipher.Decrypt(CipherText, key);
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
            using (StreamWriter writer = File.CreateText(CipherTextFilePath))
            {
                writer.Write(CipherText);
            }
        }

        public void ReadPlainText()
        {
            using (StreamReader reader = File.OpenText(PlainTextFilePath))
            {
                PlainText = reader.ReadToEnd().ToArray();
            }
        }

        public void WritePlainText()
        {
            using (StreamWriter writer = File.CreateText(PlainTextFilePath))
            {
                writer.Write(PlainText);
            }
        }

        public void Dispose() { }
    }
    
    //Text class wrapper for just encryption
    public class Encryptor : IDisposable
    {
        private Text textObject;
        
        private bool encrypted;

        public char[] PlainText
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

        public Encryptor(ICipher newCipher)
        {
            textObject = new Text(newCipher);
            encrypted = false;
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

        public void ReadPlainText()
        {
            textObject.ReadPlainText();
        }

        public void WriteCipherText()
        {
            if (true == encrypted)
            {
                textObject.WriteCipherText();
            }
            else
            {
                throw new InvalidOperationException("Text hasn't been encrypted");
            }
        }

        public void Dispose() { }
    }

    //Text class wrapper for just decryption
    public class Decryptor : IDisposable
    {
        private Text textObject;

        private bool decrypted;

        public char[] PlainText
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
            set
            {
                textObject.CipherText = value;
                decrypted = false;
            }
        }

        public List<SubKey> KeyStructure
        {
            get
            {
                return textObject.KeyStructure;
            }
        }

        public Decryptor(ICipher newCipher)
        {
            textObject = new Text(newCipher);
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

        public void ReadCipherText()
        {
            textObject.ReadCipherText();
        }

        public void WritePlainText()
        {
            if (true == decrypted)
            {
                textObject.WritePlainText();
            }
            else
            {
                throw new InvalidOperationException("Text hasn't been decrypted");
            }
        }

        public void Dispose() { }
    }
}
