using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;
using Encryption.Ciphers;

using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText;
            TKey key = new TKey();
            int input;

            bool valid;

            Console.Write("Enter some text to be encrypted: ");
            plainText = Console.ReadLine();

            valid = false;

            while (false == valid)
            {
                Console.Write("\nEnter an integer width: ");
                valid = int.TryParse(Console.ReadLine(), out input);
                key.Width = input;
            }

            valid = false;

            while (false == valid)
            {
                Console.Write("\nEnter an integer height: ");
                valid = int.TryParse(Console.ReadLine(), out input);
                key.Height = input;
            }

            using (var myEncryptor = Encryptor.New(new TCipher(), key))
            {
                myEncryptor.PlainText = plainText;
                myEncryptor.Encrypt();

                Console.WriteLine($"This is what your message looks like when encrypted:\n{new string(myEncryptor.CipherText)}");

                myEncryptor.SaveCipherText("D:\\Users\\Pascal\\Documents\\mytest.cph");
            }

            Console.WriteLine("Attempting to decrypt...");

            using (var myDecryptor = Decryptor.New(new TCipher(), key))
            {
                myDecryptor.GetCipherText("D:\\Users\\Pascal\\Documents\\mytest.cph");
                myDecryptor.Decrypt();

                Console.WriteLine($"Decrypted message:\n{myDecryptor.PlainText}");
            }

            File.Delete("D:\\Users\\Pascal\\Documents\\mytest.cph");
        }
    }
}
