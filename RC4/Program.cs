using System;
using System.IO;
using System.Text;

namespace RC4
{
    class Program
    {
        static void Main(string[] args)
        {
            string secim;
            do
            {
                Console.WriteLine("Yapilacak islemi secin.");
                Console.WriteLine("1: Dosya konumu ile plaintext ve key değerlerini girin");
                Console.WriteLine("2: Plaintext ve key değerini el ile girin");
                Console.WriteLine("3: Çıkış");
                Console.WriteLine();
                secim = Console.ReadLine();
                Secim(secim);
            } while (secim != "3");
        }
        public static void Secim(string secim)
        {
            string plainText;
            string key;
            byte[] encrypted;
            string decrypted;
            int hataSayisi;
            if (secim == "1")
            {
                hataSayisi = 0;
            plaintextStart:
                Console.WriteLine("Lutfen plaintext dosyasının, dosya konumunu giriniz");
                string plainTextFolder = Console.ReadLine();
                if (!File.Exists($@"{plainTextFolder}"))
                {
                    Console.WriteLine("Girilen dosya gecersiz.");
                    if (hataSayisi++ == 2)
                    {
                        Console.WriteLine("3 kez yanlıs giris yaptınız, menuye geri donuyorsunuz.\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{hataSayisi} kez hatalı dosya girdiniz, 3 üst üste hata yaparsanız menüye geri döneceksiniz.");
                        goto plaintextStart;
                    }
                }
                hataSayisi = 0;
            keyStart:
                Console.WriteLine("Lutfen key dosyasının, dosya konumunu giriniz");
                string keyFolder = Console.ReadLine();

                if (!File.Exists($@"{keyFolder}"))
                {
                    Console.WriteLine("Girilen dosya gecersiz.");
                    if(hataSayisi++ == 2)
                    {
                        Console.WriteLine("3 kez yanlıs giris yaptınız, menuye geri donuyorsunuz.\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{hataSayisi} kez hatalı dosya girdiniz, 3 üst üste hata yaparsanız menüye geri döneceksiniz.");
                        goto keyStart;
                    }                   
                }
                plainText = File.ReadAllText($@"{plainTextFolder}");
                key = File.ReadAllText($@"{keyFolder}");
                encrypted = RC4.Encrypt(key, plainText);
                Console.WriteLine();
                Console.Write("Sifrelenmis veri: ");
                foreach (var item in encrypted)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                decrypted = RC4.Decrypt(key, encrypted);
                Console.WriteLine("Sifresi cozulmus veri: " + decrypted);
            }
            else if (secim == "2")
            {
                Console.WriteLine("Lutfen plaintext degerini giriniz.");
                plainText = Console.ReadLine();
                Console.WriteLine("Luften key degerini giriniz");
                key = Console.ReadLine();
                encrypted = RC4.Encrypt(key, plainText);
                Console.WriteLine();
                Console.Write("Sifrelenmis veri: ");
                foreach (var item in encrypted)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                decrypted = RC4.Decrypt(key, encrypted);
                Console.WriteLine("Sifresi cozulmus veri: " + decrypted);
            }
            else
            {
                Console.WriteLine("Lutfen gecerli bir islem seciniz");
            }
            Console.WriteLine();
        }
    }
}
