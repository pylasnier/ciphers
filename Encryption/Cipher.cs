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
        char[] Decrypt(char[] cipherText, List<dynamic> key);
    }

    //Attribute that should be used to identify ciphers
    [AttributeUsage(AttributeTargets.Class,
                    AllowMultiple = false)]
    public class CipherClass : Attribute
    {
        public string Name { get; }

        public Version Version { get; }

        public Version Compatible { get; }

        public CipherClass(string newName, Version newVersion, Version newCompatible)
        {
            Name = newName;
            Version = newVersion;
            Compatible = newCompatible;
        }

        public CipherClass(string newName, string newVersion, string newCompatible)
        {
            Name = newName;
            Version = new Version(newVersion);
            Compatible = new Version(newCompatible);
        }
    }
}
