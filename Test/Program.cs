using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;
using Encryption.Ciphers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText;
            int key;

            bool valid;

            Console.Write("Enter some text to be encrypted: ");
            plainText = Console.ReadLine();

            valid = false;
            key = 0;

            while (false == valid)
            {
                Console.Write("\nEnter an integer key: ");
                valid = int.TryParse(Console.ReadLine(), out key);
            }

            using (var myEncryptor = Encryptor.New(new CCipher(), key))
            {
                myEncryptor.PlainText = plainText;
                myEncryptor.Encrypt();

                Console.WriteLine($"This is what your message looks like when encrypted:\n{new string(myEncryptor.CipherText)}");

                myEncryptor.SaveCipherText("N:\\My Documents\\mytest.cph");
            }

            Console.WriteLine("Attempting to decrypt...");

            using (var myDecryptor = Decryptor.New(new CCipher(), key))
            {
                myDecryptor.GetCipherText("N:\\My Documents\\mytest.cph");
                myDecryptor.Decrypt();

                Console.WriteLine($"Decrypted message:\n{myDecryptor.PlainText}");
            }
        }
    }
}
