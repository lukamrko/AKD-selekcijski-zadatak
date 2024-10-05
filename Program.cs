namespace CSV_Obrada
{
    class Program
    {
        private const char csvSeparator = ',';

        static void Main()
        {
            var csvFilePath = GetCSVFilePath();

            using (var reader = new StreamReader(csvFilePath))
            {
                var headerLine = reader.ReadLine();
                Console.WriteLine(headerLine);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineValues = line.Split(csvSeparator);

                    var firstName = lineValues[0].ToUpper();
                    var lastName = lineValues[1].ToUpper();
                    var birthDate = DateTime.Parse(lineValues[2]);


                }
            }
                


        }

        private static string GetCSVFilePath()
        {
            Console.WriteLine("Insert path to CSV file:");
            var csvFilePath = Console.ReadLine();

            while (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"CSV file doesn't exits at the given file path: {csvFilePath}");
                csvFilePath = Console.ReadLine();
            }

            return csvFilePath;
        }
    }
}
