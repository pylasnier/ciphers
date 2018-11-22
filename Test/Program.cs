using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;
using Encryption.Text;

using System.IO;
using System.Reflection;

namespace Test
{
    class Program
    {
        static void Main()
        {
            bool valid;
            int i;
            List<Type> ciphers;
            ICipher cipher;
            Assembly assembly;
            Type[] types;

            var key = new List<object>();
            ciphers = new List<Type>();

            assembly = Assembly.Load("caesarcipher");
            types = assembly.GetTypes();

            foreach (Type type in types)
            {
                foreach (Attribute attribute in Attribute.GetCustomAttributes(type))
                {
                    if (attribute is CipherClass)
                    {
                        ciphers.Add(type);
                    }
                }
            }

            assembly = Assembly.Load("transpositioncipher");
            types = assembly.GetTypes();

            foreach (Type type in types)
            {
                foreach (Attribute attribute in Attribute.GetCustomAttributes(type))
                {
                    if (attribute is CipherClass cipherClass)
                    {
                        Console.WriteLine(cipherClass.Name);
                        ciphers.Add(type);
                    }
                }
            }
            
            cipher = Activator.CreateInstance(ciphers[0]) as ICipher;

            if (null == cipher)
            {
                throw new InvalidCastException("Cipher isn't of valid type");
            }

            using (var MyTextObject = new Encryptor(cipher))
            {
                MyTextObject.PlainTextFilePath = "IsTestYes.txt";
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

                MyTextObject.CipherTextFilePath = "TestMeDaddy.txt";
                MyTextObject.WriteCipherText();
            }

            File.Delete("TestMeDaddy.txt");

            Console.ReadKey();
        }
    }
}
