namespace BankReference
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the reference number base (max 18 numerot): ");
            string baseNumber = Console.ReadLine();

            if (IsValidBaseNumber(baseNumber))
            {
                int checksum = CalculateChecksum(baseNumber);
                string referenceNumber = FormatReferenceNumber(baseNumber, checksum);
                Console.WriteLine("Reference number: " + referenceNumber);
            }
            else
            {
                Console.WriteLine("Invalid reference number base.");
            }
        }

        static bool IsValidBaseNumber(string baseNumber)
        {
            // Check that the base number consists only of digits and is not empty.
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

            int checksum = (10 - (sumOfProducts % 10)) % 10; // Calculate the checksum.

            return checksum;
        }

        static string FormatReferenceNumber(string baseNumber, int checksum)
        {
            // Ensure that the baseNumber is at least 19 characters long by padding it with zeros if necessary.
            while (baseNumber.Length < 18)
            {
                baseNumber = "0" + baseNumber;
            }

            // Format the reference number as desired.
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