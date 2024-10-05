namespace CSV_Obrada
{
    class Program
    {
        private const char _csvSeparator = ',';

        static void Main()
        {
            var csvFilePath = GetCSVFilePath();
            var usersData = GetUsersData(csvFilePath);
            var fullHeader = "Ime, Prezime, DatumRodjenja, Ime(hex), Prezime(hex), DatumRodjenja(hex)";

            var folderPath = Path.GetDirectoryName(csvFilePath);
            var newFileName = $"{Guid.NewGuid().ToString()}.csv";
            var newFilePath = Path.Combine(folderPath, newFileName);

            var modifiedLines = new List<string> { fullHeader };
            foreach(var userData in usersData) 
            {
                modifiedLines.Add(userData.ToCsvLine());
            }

            WriteAndShowData(newFilePath, modifiedLines);
        }

        private static void WriteAndShowData(string newFilePath, List<string> modifiedLines)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(newFilePath))
                {
                    foreach (var line in modifiedLines)
                    {
                        Console.WriteLine(line);
                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine($"File saved successfully at: {newFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

        private static List<UserData> GetUsersData(string csvFilePath)
        {
            var usersData = new List<UserData>();
            using (var reader = new StreamReader(csvFilePath))
            {
                var headerLine = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineValues = line.Split(_csvSeparator);

                    var firstName = lineValues[0].ToUpper();
                    var lastName = lineValues[1].ToUpper();
                    var birthDate = DateTime.Parse(lineValues[2]);

                    var userData = new UserData
                    {
                        Name = firstName,
                        Surname = lastName,
                        BirthDate = birthDate,
                    };

                    usersData.Add(userData);
                }
            }
            return usersData;
        }
    }
}
