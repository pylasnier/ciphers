using System.Linq;

namespace Encryption
{
    namespace Ciphers
    {
        //Caesar Cipher
        //Shifts character's numeric value by a given number of places
        public class CCipher : ICipher<int>
        {
            public char[] Encrypt(char[] plainText, int key)
            {
                char[] cipherText;
                int i;

                key = key % char.MaxValue;
                cipherText = new char[plainText.Length];

                for (i = 0; i < plainText.Length; i++)
                {
                    //Character shift operation
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
                return Encrypt(cipherText, -key);
            }

            public char[] Decrypt(string cipherText, int key)
            {
                return Encrypt(cipherText, -key);
            }
        }
    }
}
