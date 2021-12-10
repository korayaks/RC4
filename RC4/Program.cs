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
                Console.WriteLine("1: Dosya konumu ile duzyazi ve anahtar değerlerini girin");
                Console.WriteLine("2: duzyazi ve anahtar değerini el ile girin");
                Console.WriteLine("3: Çıkış");
                Console.WriteLine();
                secim = Console.ReadLine();
                Secim(secim);
            } while (secim != "3");
        }
        public static void Secim(string secim)
        {
            string duzyazi;
            string anahtar;
            byte[] sifrelenmis;
            string sifresiCozulmus;
            int hataSayisi;
            if (secim == "1")
            {
                hataSayisi = 0;
            duzyaziStart:
                Console.WriteLine("Lutfen düzyazı dosyasının, dosya konumunu giriniz");
                string duzyaziDosya = Console.ReadLine();
                if (!File.Exists($@"{duzyaziDosya}"))
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
                        goto duzyaziStart;
                    }
                }
                hataSayisi = 0;
            anahtarStart:
                Console.WriteLine("Lutfen anahtar dosyasının, dosya konumunu giriniz");
                string anahtarDosya = Console.ReadLine();

                if (!File.Exists($@"{anahtarDosya}"))
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
                        goto anahtarStart;
                    }                   
                }
                duzyazi = File.ReadAllText($@"{duzyaziDosya}");
                anahtar = File.ReadAllText($@"{anahtarDosya}");
                sifrelenmis = RC4.Encrypt(anahtar, duzyazi);
                Console.WriteLine();
                Console.Write("Sifrelenmis veri: ");
                foreach (var item in sifrelenmis)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                sifresiCozulmus = RC4.Decrypt(anahtar, sifrelenmis);
                Console.WriteLine("Sifresi cozulmus veri: " + sifresiCozulmus);
            }
            else if (secim == "2")
            {
                Console.WriteLine("Lutfen düzyazı degerini giriniz.");
                duzyazi = Console.ReadLine();
                Console.WriteLine("Luften anahtar degerini giriniz");
                anahtar = Console.ReadLine();
                sifrelenmis = RC4.Encrypt(anahtar, duzyazi);
                Console.WriteLine();
                Console.Write("Sifrelenmis veri: ");
                foreach (var item in sifrelenmis)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                sifresiCozulmus = RC4.Decrypt(anahtar, sifrelenmis);
                Console.WriteLine("Sifresi cozulmus veri: " + sifresiCozulmus);
            }
            else
            {
                Console.WriteLine("Lutfen gecerli bir islem seciniz");
            }
            Console.WriteLine();
        }
    }
}
