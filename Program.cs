using System.Globalization;

namespace CSV_Obrada
{
    class Program
    {
        static void Main()
        {
            var csvFilePath = GetCSVFilePath();
            var csvSeparator = ConfigService.GetCSVSeparator();

            var usersData = GetUsersData(csvFilePath, csvSeparator);

            var newFilePath = csvFilePath;
            do
            {
                newFilePath = GetNewFilePath(newFilePath);
            } while (File.Exists(newFilePath));

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
                var culture = new CultureInfo("hr-HR");
                while ((line = reader.ReadLine()) != null)
                {
                    var lineValues = line.Split(csvSeparator);
                    if (lineValues.Length != 3)
                    {
                        Console.WriteLine($"Skiping line becuase of inproper number of arguments: {line}");
                        continue;
                    }
                    
                    var firstName = lineValues[0].Trim();
                    var lastName = lineValues[1].Trim();
                    var birthDate = DateTime.Now;
                    try
                    {
                        birthDate = DateTime.Parse(lineValues[2], culture);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Skipping the line because an error occured while converting date {lineValues[2]} at line:{line}");
                    }
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
                Console.WriteLine();
                using (var writer = new StreamWriter(newFilePath))
                {
                    foreach (var line in modifiedLines)
                    {
                        Console.WriteLine(line);
                        writer.WriteLine(line);
                    }
                }
                Console.WriteLine($"\nFile saved successfully at: {newFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
