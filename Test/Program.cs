using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Encryption;
using Ciphers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CCipher myCipher = new CCipher();
            Text<CCipher, myCipher.KeyType> myText;

            myText.SetPlainText("Hello world!");

            myText.Encrypt(4);

            myText.GetCipherText();
        }
    }
}
