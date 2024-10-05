using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Obrada
{
    public class UserData
    {
        private const char _CSVseparator = ',';
        private const string _birthDateFormat = "dd-MM-yyyy";

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public string ToCsvLine()
        {
            var sb = new StringBuilder();
            var bigName = Name.ToUpper();
            sb.Append(bigName);
            sb.Append($"{_CSVseparator} ");

            var bigSurname = Surname.ToUpper();
            sb.Append(bigSurname);
            sb.Append($"{_CSVseparator} ");

            var birthDateText = BirthDate.ToString(_birthDateFormat);
            sb.Append(birthDateText);
            sb.Append($"{_CSVseparator} ");

            sb.Append(StringToHex(bigName));
            sb.Append($"{_CSVseparator} ");

            sb.Append(StringToHex(bigSurname));
            sb.Append($"{_CSVseparator} ");

            sb.Append(StringToHex(birthDateText));
            sb.Append($"{_CSVseparator} ");

            return sb.ToString();
        }

        private string StringToHex(string input)
        {
            var hexBuilder = new StringBuilder();
            foreach (char c in input)
            {
                hexBuilder.AppendFormat("{0:X2}", (int)c);
            }
            return hexBuilder.ToString();
        }

    }
}
