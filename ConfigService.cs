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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
