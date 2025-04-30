/*
This is based on GPL code, but MIT is allowed by the original author:
-> https://games.technoplaza.net/ggencoder/js/
- Copyright (C) 2004-2006, 2008, 2016 emuWorks (http://games.technoplaza.net/)
- This work is licensed under the GNU General Public License v2.

John Ratliff <**********>
Tue, Apr 1, 4:56 PM
to me
I'm fine if you want to include your port in an MIT licensed project
so long as you reference the original.
--John
*/

namespace GBggDecoder
{
    public class GBGameGenieHandler
    {
        const string ALPHABET_GBGG = "0123456789ABCDEF";

        public static FormattedCode FormatRawCode(RawCode code)
        {
            var formatted = new FormattedCode
            {
                Value = code.Value.ToString("X"),
                Address = code.Address.ToString("X"),
                Compare = code.Compare?.ToString("X") ?? string.Empty
            };

            if (code.Value != 0)
            {
                formatted.Value = formatted.Value.PadLeft(2, '0').ToUpper();
            }

            if (code.Address != 0)
            {
                formatted.Address = formatted.Address.PadLeft(4, '0').ToUpper();
            }

            if (code.Compare != null)
            {
                formatted.Compare = formatted.Compare.PadLeft(2, '0').ToUpper();
            }

            return formatted;
        }

        public static RawCode DecodeGBGG(string code)
        {
            var rawCode = new RawCode();

            code = code.ToUpper();
            if (IsValidGBGGCode(code))
            {
                var hex = new int[9];

                int hexIndex = 0;
                for (int i = 0; i < code.Length; i++)
                {
                    if (i == 3 || i == 7) continue;
                    hex[hexIndex++] = ALPHABET_GBGG.IndexOf(code[i]);
                }

                rawCode.Value = (hex[0] << 4) | hex[1];
                rawCode.Address = (hex[2] << 8) | (hex[3] << 4) | hex[4] | ((~hex[5] & 0xF) << 12);

                if (code.Length == 11)
                {
                    int temp = (hex[6] << 4) | hex[8];
                    temp = (temp >> 2) | ((temp << 6) & 0xFC);
                    rawCode.Compare = (temp ^ 0xBA);
                }

                return rawCode;
            }
            else
            {
                return null;
            }
        }

        public static bool IsValidGBGGCode(string code)
        {
            // GBGG codes are either 7 or 11 characters
            if (code.Length != 7 && code.Length != 11)
            {
                return false;
            }

            if (code[3] != '-' || (code.Length == 11 && code[7] != '-'))
            {
                return false;
            }

            foreach (char c in code)
            {
                if (c == '-' || ALPHABET_GBGG.Contains(c))
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        public class RawCode
        {
            public int Address { get; set; }
            public int Value { get; set; }
            public int? Compare { get; set; } = null;
        }

        public class FormattedCode
        {
            public string Address { get; set; }
            public string Value { get; set; }
            public string Compare { get; set; }
        }
    }
}
