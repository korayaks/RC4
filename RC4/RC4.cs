using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC4
{
    public static class RC4
    {
        public static byte[] Encrypt(string key, string veri)
        {
            byte[] byteKey = Encoding.ASCII.GetBytes(key);
            byte[] bytePlainText = Encoding.ASCII.GetBytes(veri);            
            return Encrypt(byteKey, bytePlainText);
        }

        public static string Decrypt(string key, byte[] veri)
        {
            byte[] byteKey = Encoding.ASCII.GetBytes(key);
            var decrypted = Encrypt(byteKey, veri);
            char[] cipherText = new char[decrypted.Length];
            for (int i = 0; i < decrypted.Length; i++)
            {
                cipherText[i] = Convert.ToChar(decrypted[i]);
            }         
            return new string(cipherText);
        }

        public static byte[] Encrypt(byte[] key, byte[] veri)
        {
            return EncryptOutput(key, veri).ToArray();
        }

        public static byte[] Decrypt(byte[] key, byte[] veri)
        {
            return EncryptOutput(key, veri).ToArray();
        }

        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                YerDegistir(s, i, j);
            }

            return s;
        }

        private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> veri)
        {
            byte[] s = EncryptInitalize(key);

            int i = 0;
            int j = 0;

            return veri.Select((b) =>
            {
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;

                YerDegistir(s, i, j);

                return (byte)(b ^ s[(s[i] + s[j]) & 255]);
            });
        }

        private static void YerDegistir(byte[] s, int i, int j)
        {
            byte c = s[i];
            s[i] = s[j];
            s[j] = c;
        }
    }
}
