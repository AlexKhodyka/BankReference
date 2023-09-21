namespace BankReference
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Syötä viitenumeron pohja (enintään 18 numeroa): ");
            string baseNumber = Console.ReadLine();

            if (IsValidBaseNumber(baseNumber))
            {
                int checksum = CalculateChecksum(baseNumber);
                string referenceNumber = FormatReferenceNumber(baseNumber, checksum);
                Console.WriteLine("Viitenumero: " + referenceNumber);
            }
            else
            {
                Console.WriteLine("Virheellinen viitenumeropohja.");
            }
        }

        static bool IsValidBaseNumber(string baseNumber)
        {
            // Tarkista, että perusnumero koostuu vain numeroista eikä ole tyhjä.
            return !string.IsNullOrWhiteSpace(baseNumber) && baseNumber.All(char.IsDigit);
        }

        static int CalculateChecksum(string baseNumber)
        {
            int[] weights = { 7, 3, 1, 7, 3, 1, 7, 3, 1 };
            int sumOfProducts = 0;

            for (int i = 0; i < baseNumber.Length; i++)
            {
                int digit = int.Parse(baseNumber[i].ToString());
                sumOfProducts += digit * weights[i % 9];
            }

            int checksum = (10 - (sumOfProducts % 10)) % 10; // Laske tarkistussumma.

            return checksum;
        }

        static string FormatReferenceNumber(string baseNumber, int checksum)
        {
            // Varmista, että baseNumber on vähintään 19 merkkiä pitkä ja täydennä se tarvittaessa nollilla.
            while (baseNumber.Length < 18)
            {
                baseNumber = "0" + baseNumber;
            }
            string referenceNumber = string.Format("{0} {1} {2} {3} {4} {5}",
                baseNumber.Substring(0, 2),
                baseNumber.Substring(2, 5),
                baseNumber.Substring(7, 5),
                baseNumber.Substring(12, 5),
                baseNumber.Substring(17, 1),
                checksum);
            return referenceNumber;
        }
    }
}