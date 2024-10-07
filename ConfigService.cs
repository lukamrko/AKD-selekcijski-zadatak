using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace CSV_Obrada
{
    public static class ConfigService
    {
        private static readonly Lazy<IConfigurationRoot> _configurationLazy = new Lazy<IConfigurationRoot>(LoadConfiguration);

        private static IConfigurationRoot Configuration => _configurationLazy.Value;

        public static char GetCSVSeparator()
        {
            return Configuration.GetValue<char>("CsvSeparator");
        }

        public static bool GetContainsHeader()
        {
            return Configuration.GetValue<bool>("TargetFileContainsHeader");
        }

        public static string GetBirthDateFormat()
        {
            return Configuration.GetValue<string>("BirthDateFormat");
        }

        public static string GetModifiedFileAppendix()
        {
            return Configuration.GetValue<string>("ModifiedFileAppendix");
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;

            // Go up to the project directory
            var projectDirectory = Directory.GetParent(assemblyPath).Parent.Parent.Parent.FullName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
