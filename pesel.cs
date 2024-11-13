using System;

namespace Pesel_Bialy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string imie = Imie();
                string nazwisko = Nazwisko();

                if (!DataUrodzenia())
                    break;

                string plec = GetPlec();
                if (plec == null)
                    break;

                string pesel = GetPesel();
                if (pesel == null)
                    break;

                if (!ValidateControlSum(pesel))
                {
                    Console.WriteLine("Zła suma kontrolna");
                    break;
                }

                Console.WriteLine($"PESEL {pesel} jest prawidłowy");
            }
        }

        static string Imie()
        {
            Console.WriteLine("Podaj imie: ");
            string imie = Console.ReadLine();
            imie = char.ToUpper(imie[0]) + imie.Substring(1).Replace(" ", "");
            return imie;
        }

        static string Nazwisko()
        {
            Console.WriteLine("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();
            nazwisko = char.ToUpper(nazwisko[0]) + nazwisko.Substring(1).Replace("-", "");
            return nazwisko;
        }

        static string DataUrodzenia()
        {
            Console.WriteLine("data urodzenia yyyy-m-d:");
            string dateInput = Console.ReadLine();

            if (DateTime.TryParse(dateInput, out DateTime birthDate))
            {
                Console.WriteLine($"Data urodzenia: {birthDate:yyyy-MM-dd}");
                return true;
            }
            else
            {
                Console.WriteLine("zła data");
                return false;
            }
        }

        static string GetPlec()
        {
            Console.WriteLine("Podaj płeć (K/k - kobieta, M/m - mężczyzna):");
            string plec = Console.ReadLine().ToUpper();

            if (plec == "K")
            {
                Console.WriteLine("Kobieta");
                return "K";
            }
            else if (plec == "M")
            {
                Console.WriteLine("Mężczyzna");
                return "M";
            }
            else
            {
                Console.WriteLine("Złe dane");
                return null;
            }
        }

        static string GetPesel()
        {
            Console.WriteLine("Podaj PESEL: ");
            string pesel = Console.ReadLine();

            if (pesel.Length != 11)
            {
                Console.WriteLine("Zły PESEL, wymagane 11 cyfr.");
                return null;
            }
            return pesel;
        }

        static bool ValidateControlSum(string pesel)
        {
            int sumaKontrolna = 0;
            int[] mnozniki = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            for (int i = 0; i < 10; i++)
            {
                sumaKontrolna += int.Parse(pesel[i].ToString()) * mnozniki[i];
            }

            int kontrolnaCyfra = 10 - (sumaKontrolna % 10);
            if (kontrolnaCyfra == 10) kontrolnaCyfra = 0;

            return kontrolnaCyfra == int.Parse(pesel[10].ToString());
        }
    }
}
