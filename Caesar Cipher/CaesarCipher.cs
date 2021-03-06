﻿using System;
using System.Collections.Generic;
using System.Linq;

using Encryption;

namespace Ciphers
{
    //Caesar Cipher
    //Shifts character's numeric value by a given number of places
    [CipherClass("Caesar Cipher", "1.0", "1.0")]
    public class CCipher : ICipher
    {
        public List<SubKey> KeyStructure
        {
            get
            {
                List<SubKey> returnValue = new List<SubKey>();
                returnValue.Add(new SubKey(typeof(int), "shift"));
                return returnValue;
            }
        }

        char[] encrypt(char[] plainText, int shift)
        {
            char[] cipherText;
            int i;

            shift = shift % char.MaxValue;
            cipherText = new char[plainText.Length];

            for (i = 0; i < plainText.Length; i++)
            {
                //Character shift operation
                cipherText[i] = (char)(plainText[i] + shift);
            }

            return cipherText;
        }

        public char[] Encrypt(char[] plainText, List<dynamic> key)
        {
            return encrypt(plainText, key[0]);
        }

        public char[] Decrypt(char[] cipherText, List<dynamic> key)
        {
            return encrypt(cipherText, -key[0]);
        }
    }
}
