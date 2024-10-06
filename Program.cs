namespace CSV_Obrada
{
    class Program
    {
        static void Main()
        {
            var csvFilePath = GetCSVFilePath();
            var csvSeparator = ConfigService.GetCSVSeparator();

            var usersData = GetUsersData(csvFilePath, csvSeparator);

            string newFilePath = GetNewFilePath(csvFilePath);

            var modifiedLines = GetNewModifiedLines(csvSeparator, usersData);

            WriteAndShowData(newFilePath, modifiedLines);
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

        private static List<UserData> GetUsersData(string csvFilePath, char csvSeparator)
        {
            var usersData = new List<UserData>();
            var containsHeader = ConfigService.GetContainsHeader();

            using (var reader = new StreamReader(csvFilePath))
            {
                if (containsHeader) 
                {
                    reader.ReadLine();
                }
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineValues = line.Split(csvSeparator);

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

        private static string GetNewFilePath(string csvFilePath)
        {
            var folderPath = Path.GetDirectoryName(csvFilePath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(csvFilePath);
            var newFileAppendix = ConfigService.GetModifiedFileAppendix();
            var newFileName = $"{fileNameWithoutExtension}{newFileAppendix}.csv";
            var newFilePath = Path.Combine(folderPath, newFileName);
            return newFilePath;
        }

        private static List<string> GetNewModifiedLines(char csvSeparator, List<UserData> usersData)
        {
            var fullHeader = $"Ime{csvSeparator} Prezime{csvSeparator} DatumRodjenja{csvSeparator} Ime(hex){csvSeparator} Prezime(hex){csvSeparator} DatumRodjenja(hex)";
            var modifiedLines = new List<string> { fullHeader };
            var birthDateFormat = ConfigService.GetBirthDateFormat();
            foreach (var userData in usersData)
            {
                var userDataLine = userData.ToCsvLine(csvSeparator, birthDateFormat);
                modifiedLines.Add(userDataLine);
            }

            return modifiedLines;
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
    }
}
