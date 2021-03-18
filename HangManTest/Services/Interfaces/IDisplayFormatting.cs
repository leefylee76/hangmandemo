using System.Collections.Generic;

namespace HangMan.Services
{
    public interface IDisplayFormatting
    {
        string FormatLettersForDisplay(string letters);
        string FormatLettersForDisplay(List<char> letters);
    }
}
