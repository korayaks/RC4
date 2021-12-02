using System;
using System.Text;

namespace RC4
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = "rc4 algorithm";
            string key = "korayaks";
            var encrypted = RC4.Encrypt(key, plainText);
            foreach (var item in encrypted)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            var decrypted = RC4.Decrypt(key, encrypted);
            char[] cipherText = new char[decrypted.Length];
            for (int i = 0; i < decrypted.Length; i++)
            {
                cipherText[i] = Convert.ToChar(decrypted[i]);
            }
            foreach (var item in cipherText)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
