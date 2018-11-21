using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;
using Encryption.Text;

using System.IO;

namespace Test
{
    class Program
    {
        static void Main()
        {
            bool valid;
            int i;

            var key = new List<object>();

            ICipher myCipher = (ICipher) Activator.CreateComInstanceFrom("caesarcipher.dll", "Ciphers.CCipher").Unwrap();

            using (var MyTextObject = new Encryptor(myCipher))
            {
                MyTextObject.PlainTextFilePath = "N:\\My Documents\\IsTestYes.txt";
                MyTextObject.ReadPlainText();

                for (i = 0; i < MyTextObject.KeyStructure.Count; i++)
                {
                    valid = false;
                    while (false == valid)
                    {
                        Console.Write($"{MyTextObject.KeyStructure[i].Name}: ");
                        try
                        {
                            key.Add(Convert.ChangeType(Console.ReadLine(), MyTextObject.KeyStructure[i].KeyType));
                            valid = true;
                        }
                        catch
                        {
                            Console.WriteLine("Wrong type of input given");
                        }
                    }
                }

                MyTextObject.Encrypt(key);

                Console.Write("\n\n\n");
                Console.WriteLine(MyTextObject.CipherText);

                MyTextObject.CipherTextFilePath = "N:\\My Documents\\TestMeDaddy.txt";
                MyTextObject.WriteCipherText();
            }

            File.Delete("N:\\My Documents\\TestMeDaddy.txt");

            Console.ReadKey();
        }
    }
}
