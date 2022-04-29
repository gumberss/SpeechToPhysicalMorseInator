using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToPhysicalInator
{
    internal class MorseService
    {
        private static Dictionary<char, String> morse = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Zip(new[]
        {//0 = . / 1 = - / 2 = space or "wait time"
            "01",
            "1000",
            "1010",
            "100",
            "0",
            "0010",
            "110",
            "0000",
            "00",
            "0111",
            "101",
            "0100",
            "11",
            "10",
            "111",
            "0110",
            "1101",
            "010",
            "000",
            "1",
            "001",
            "0001",
            "011",
            "1001",
            "1011",
            "1100"
        })
            .ToDictionary(x => x.First, x => x.Second);

        public String ParseText(String text) => text
            .ConvertToAsc2()
            .ToUpper()
            .Split(' ')
            .SelectMany(x => x.Select(y => morse.ContainsKey(y) ? morse[y] + "2" : ""))
            .Aggregate(String.Concat);
    }

    public static class TextExtension
    {
        public static String ConvertToAsc2(this String text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
