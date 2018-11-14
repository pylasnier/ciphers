using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    //Used to convey information about key type and use
    public class SubKey
    {
        public Type KeyType { get; }
        public string Name { get; }

        public SubKey(Type keyType, string name)
        {
            KeyType = keyType;
            Name = name;
        }
    }

    //Interface used to write cipher classes
    public interface ICipher
    {
        List<SubKey> KeyStructure { get; }

        char[] Encrypt(char[] plainText, List<dynamic> key);
        char[] Encrypt(string plainText, List<dynamic> key);
        char[] Decrypt(char[] cipherText, List<dynamic> key);
        char[] Decrypt(string cipherText, List<dynamic> key);
    }
}
