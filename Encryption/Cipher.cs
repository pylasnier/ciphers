namespace Encryption
{
    //Interface used to write cipher classes
    public interface ICipher<T>
    {
        char[] Encrypt(char[] plainText, T key);
        char[] Encrypt(string plainText, T key);
        char[] Decrypt(char[] cipherText, T key);
        char[] Decrypt(string cipherText, T key);
    }
}
