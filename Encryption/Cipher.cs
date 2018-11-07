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
        public Type KeyType { get; set; }
        public string Name { get; set; }

        public SubKey(Type keyType, string name)
        {
            KeyType = keyType;
            Name = name;
        }
    }

    //Interface used to write cipher classes
    public interface ICipher<T>
    {
        List<SubKey> KeyStructure { get; }

        char[] Encrypt(IEnumerable<object> vals);
        char[] Encrypt(string plainText, T key);
        char[] Decrypt(char[] cipherText, T key);
        char[] Decrypt(string cipherText, T key);
    }
}
