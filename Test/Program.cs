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

            using (var MyTextObject = new Encryptor("caesarcipher.dll", "Encryption.Ciphers.CCipher"))
            {
                MyTextObject.PlainText = "Hello world!";

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

                MyTextObject.SaveCipherText("N:\\My Documents\\TestMeDaddy.txt");
            }

            File.Delete("N:\\My Documents\\TestMeDaddy.txt");

            Console.ReadKey();
        }
    }
}
