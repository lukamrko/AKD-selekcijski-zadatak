using System.Text;

namespace CSV_Obrada
{
    public class UserData
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public string ToCsvLine(char csvSeparator, string birthDateFormat)
        {
            var sb = new StringBuilder();
            var bigName = Name.ToUpper();
            sb.Append(bigName);
            sb.Append($"{csvSeparator} ");

            var bigSurname = Surname.ToUpper();
            sb.Append(bigSurname);
            sb.Append($"{csvSeparator} ");

            var birthDateText = BirthDate.ToString(birthDateFormat);
            sb.Append(birthDateText);
            sb.Append($"{csvSeparator} ");

            sb.Append(StringToHex(bigName));
            sb.Append($"{csvSeparator} ");

            sb.Append(StringToHex(bigSurname));
            sb.Append($"{csvSeparator} ");

            sb.Append(StringToHex(birthDateText));
            sb.Append($"{csvSeparator} ");

            return sb.ToString();
        }

        private string StringToHex(string input)
        {
            return string.Concat(input.Select(c => ((int)c).ToString("X2")));
        }

    }
}
