using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var myEncryptor = new Encryptor<CCipher, int>(new CCipher());
            var myDecryptor = new Decryptor<CCipher, int>(new CCipher());

            myEncryptor.PlainText = "Hello World!";
            myEncryptor.Encrypt(15);
            Console.WriteLine(myEncryptor.CipherText);
            myEncryptor.SaveCipherText("N:\\My Documents\\test.ciph");

            myDecryptor.GetCipherText("N:\\My Documents\\test.ciph");
            myDecryptor.Decrypt(15);
            Console.WriteLine(myDecryptor.PlainText);
        }
    }
}
