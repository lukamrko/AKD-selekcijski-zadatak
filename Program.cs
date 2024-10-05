namespace CSV_Obrada
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Insert path to CSV file:");
            var filePath = Console.ReadLine();

            while (!File.Exists(filePath))
            {
                Console.WriteLine($"CSV file doesn't exits at the given file path: {filePath}");
                filePath = Console.ReadLine();
            }





        }
    }
}
