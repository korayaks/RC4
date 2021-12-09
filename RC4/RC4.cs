using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC4
{
    public static class RC4
    {
        public static byte[] Encrypt(string anahtar, string duzMetin)
        {
            byte[] byteAnahtar = Encoding.ASCII.GetBytes(anahtar);     //string gelen anahtar verisini byte dizisine dönüştürüyorum
            byte[] byteDuzMetin = Encoding.ASCII.GetBytes(duzMetin);   //string gelen düz metni byte dizisine dönüştürüyorum            
            return SifreleVeSifreCoz(byteAnahtar, byteDuzMetin).ToArray(); //şifreleme sonucu oluşan byte dizisini main'e gönderiyorum.
        }

        public static string Decrypt(string anahtar, byte[] duzMetin)
        {
            byte[] byteAnahtar = Encoding.ASCII.GetBytes(anahtar);                //string gelen anahtar verisini byte dizisine dönüştürüyorum 
            byte[] cozulmusSifre = SifreleVeSifreCoz(byteAnahtar, duzMetin).ToArray();//Şifreli metni ve anahtar dizilerini parametre vererek düz metni elde ediyorum
            char[] cozulmusDuzMetin = new char[cozulmusSifre.Length];             //RC4 algoritmasının şifreleme ve şifre çözme işlemleri aynı
                                                                                  //olduğundan aynı metodu kullanıyorum. İkisinde de XOR işlemi yapılıyor.
            for (int i = 0; i < cozulmusSifre.Length; i++)
            {
                cozulmusDuzMetin[i] = Convert.ToChar(cozulmusSifre[i]);//Decimal olan byte değerlerini ascii tablosunda karşılık gelen char değerlerine dönüştürüyorum
            }
            return new string(cozulmusDuzMetin); //şifre çözme sonucu oluşan char dizisini stringe dönüştürüp main'e gönderiyorum.
        }

        private static IEnumerable<byte> SifreleVeSifreCoz(byte[] anahtar, IEnumerable<byte> veri)
        {
            byte[] s = new byte[256];
            int i = 0;
            int j = 0;
            for (i = 0; i < s.Length; i++)
            {
                s[i] = (byte)(i); //s dizisinin içini 0 dan başlayarak 255'e kadar sırasıyla dolduruyorum.
            }

            for (i = 0, j = 0; i < 256; i++)
            {
                j = (j + anahtar[i % anahtar.Length] + s[i]) & 255;// s dizisi ve anahtar dizisini kullanarak yeni bir j değeri elde ediyorum
                                                 // s dizisindeki i'inci eleman ile j'inci elemanın yerini değiştiriyorum
                YerDegistir(s, i, j);            // Normalde anahtar dizisi kendini tekrar ederek kendini 255'e kadar uzatılması gerekir. Ben uzatmak yerinde anahtarın uzunluğu
            }                                    // Kadar mod alarak uzatmaya gerek kalmadan işlemleri tamamlamış oluyorum.
            i = 0;
            j = 0;
            return veri.Select((d) =>   //plaintextin uzunluğu ne kadar ise döngü o kadar olacaktır. while döngüsü yerine kullandım.
            {                           //stack overflow (dizi taşması) hatasını engellemek için işlemlerde 256 ya göre mod alınıyor.
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;

                YerDegistir(s, i, j);

                return (byte)(d ^ s[(s[i] + s[j]) & 255]);  // "^" operatörü ile XOR işlemi yapılıyor. "^" operatörünün sağındaki işlem sayesinde anahtar
            });                                             //bayt değerim hazır hale getirilmiş oluyor, "^" operatörünün solunda ise başta girilen plaintexte 
        }                                                   //ait olan bayt bulunuyor. Bu iki bayt "^" işlemine tabi tutularak şifrelenmiş veriye ait bayt oluşturuluyor. 

        private static void YerDegistir(byte[] s, int i, int j)
        {                   //parametrelerde verilen s bayt dizisindeki iki değerin yerini değiştiriyorum
            byte c = s[i];  //geçici c değeri s dizisinin i indisindeki değerini tutuyor
            s[i] = s[j];    //s dizisindeki i indisine, s dizisinin j indisindeki değeri atıyorum
            s[j] = c;       //s dizisindeki j indisine, geçici oluşturduğum c değerini atıyorum.
        }                   //sonuç olarak s[i] ve s[j] değerleri yer değiştiriyor
    }
}
