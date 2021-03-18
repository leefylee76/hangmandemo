using System.Collections.Generic;
using System.Text;

namespace HangMan.Services
{
    public class DisplayFormatting : IDisplayFormatting
{
        public string FormatLettersForDisplay(string letters)
        {
            if (string.IsNullOrEmpty(letters))
                return letters;

            var array = new List<char>();
            array.AddRange(letters);

            return FormatLettersForDisplay(array);
        }

        public string FormatLettersForDisplay(List<char> letters)
        {
            var sb = new StringBuilder();

            foreach (var item in letters)
            {
                sb.Append($"{item} ");
            }

            return sb.ToString();
        }
    }
}
