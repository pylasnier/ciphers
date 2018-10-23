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
            CCipher myCipher = new CCipher();
            var myText = new Text<CCipher, int>(myCipher);

            myText.SetPlainText("Hello world!");

            myText.Encrypt(4);

            Console.WriteLine(myText.GetCipherText());
        }
    }
}
