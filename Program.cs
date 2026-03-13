using System;
using System.Collections.Generic;
using System.Linq;

namespace FinansUygulamasi
{
    // 1. VERİ MODELİ: Harcama Bilgilerini Tutan Sınıf
    public class Harcama
    {
        public decimal Miktar { get; set; }
        public string Kategori { get; set; }
        public string Banka { get; set; }
        public DateTime Tarih { get; set; }

        public Harcama()
        {
            Tarih = DateTime.Now; // Her harcama eklendiğinde o anki tarihi otomatik alır
        }
    }

    public class Program
    {
        // Harcamaları RAM üzerinde tutacak ana listemiz
        static List<Harcama> harcamaListesi = new List<Harcama>();

        static void Main(string[] args)
        {
            GirisSistemi();
            AnaMenu();
        }

        // --- GÜVENLİK BÖLÜMÜ ---
        static void GirisSistemi()
        {
            Console.WriteLine("=== Cüzdanım Kayıt Ekranı ===");
            Console.Write("Kullanıcı adı belirleyin: ");
            string kayitliKullanici = Console.ReadLine();
            Console.Write("Şifre belirleyin: ");
            string kayitliSifre = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("=== Giriş Paneli ===");

            while (true)
            {
                Console.Write("Kullanıcı Adı: ");
                string girisKullanici = Console.ReadLine();
                Console.Write("Şifre: ");
                string girisSifre = Console.ReadLine();

                if (girisKullanici == kayitliKullanici && girisSifre == kayitliSifre)
                {
                    Console.WriteLine($"\nHoş geldiniz {kayitliKullanici}! Sisteme giriş yapıldı.");
                    Console.WriteLine("Devam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Hatalı kullanıcı adı veya şifre! Tekrar deneyin.");
                }
            }
        }

        // --- ANA MENÜ ---
        static void AnaMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== WALLETWISE KİŞİSEL FİNANS PANELİ ===");
                Console.WriteLine("1- Yeni Harcama Ekle");
                Console.WriteLine("2- Tüm Harcamaları Listele");
                Console.WriteLine("3- Toplam Harcamayı Gör");
                Console.WriteLine("4- Bankaya Göre Filtrele");
                Console.WriteLine("5- Çıkış");
                Console.Write("\nSeçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": HarcamaEkle(); break;
                    case "2": HarcamalariListele(); break;
                    case "3": ToplamHesapla(); break;
                    case "4": BankaFiltrele(); break;
                    case "5": Environment.Exit(0); break;
                    default: Console.WriteLine("Geçersiz seçim!"); break;
                }
                Console.WriteLine("\nMenüye dönmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }

        // --- İŞLEM METOTLARI ---

        static void HarcamaEkle()
        {
            try
            {
                Harcama yeniHarcama = new Harcama();

                Console.Write("Harcama Miktarı (Örn: 150,50): ");
                yeniHarcama.Miktar = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Kategori (Yemek, Market, vb.): ");
                yeniHarcama.Kategori = Console.ReadLine();

                Console.Write("Banka Adı (Ziraat, Akbank, vb.): ");
                yeniHarcama.Banka = Console.ReadLine();

                harcamaListesi.Add(yeniHarcama);
                Console.WriteLine("\nHarcama başarıyla kaydedildi!");
            }
            catch (Exception)
            {
                Console.WriteLine("\nHata: Lütfen geçerli bir sayı giriniz (Virgül kullanmayı unutmayın).");
            }
        }

        static void HarcamalariListele()
        {
            if (harcamaListesi.Count == 0)
            {
                Console.WriteLine("Henüz kayıtlı bir harcama bulunmuyor.");
                return;
            }

            Console.WriteLine("\n--- TÜM HARCAMALARINIZ ---");
            foreach (var h in harcamaListesi)
            {
                Console.WriteLine($"Tarih: {h.Tarih.ToShortDateString()} | Banka: {h.Banka} | Kategori: {h.Kategori} | Tutar: {h.Miktar:C2}");
            }
        }

        static void ToplamHesapla()
        {
            decimal toplam = 0;
            foreach (var h in harcamaListesi)
            {
                toplam += h.Miktar;
            }
            Console.WriteLine($"\nToplam harcama tutarınız: {toplam:C2}");
        }

        static void BankaFiltrele()
        {
            Console.Write("Görmek istediğiniz bankanın adını yazın: ");
            string arananBanka = Console.ReadLine().ToLower();

            var bulunanlar = harcamaListesi.FindAll(h => h.Banka.ToLower() == arananBanka);

            if (bulunanlar.Count == 0)
            {
                Console.WriteLine("\nBu bankaya ait harcama bulunamadı.");
            }
            else
            {
                decimal bankaToplami = 0;
                Console.WriteLine($"\n--- {arananBanka.ToUpper()} HARCAMALARI ---");
                foreach (var h in bulunanlar)
                {
                    Console.WriteLine($"Kategori: {h.Kategori} | Tutar: {h.Miktar:C2}");
                    bankaToplami += h.Miktar;
                }
                Console.WriteLine($"\nBu bankadaki toplam harcama: {bankaToplami:C2}");
                
            }
            
        }
        
    }
}