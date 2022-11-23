using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && IsDoubleQuoted(input)
                && ContainsValidChars(input);
        }

        private static bool ContainsControlCharacters(string input)
        {
            foreach (char c in input)
            {
                if (c < ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsValidChars(string input)
        {
            return !ContainsControlCharacters(input)
                && EscapedCharsAreValid(input)
                && UnicodeCharsAreValid(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool IsDoubleQuoted(string input)
        {
            return input.Length >= 2 && input.StartsWith('"') && input.EndsWith('"');
        }

        private static bool EscapedCharsAreValid(string input)
        {
            const byte numberOfItemsToRemove = 2;
            const string allowedToBeEscaped = "\"\\/bfnrtu";
            for (int i = 0; i < allowedToBeEscaped.Length; i++)
            {
                if (!input.Contains('\\'))
                {
                    return true;
                }

                if (input[input.IndexOf('\\') + 1] == allowedToBeEscaped[i]
                    && input.IndexOf('\\') + 1 != input.Length - 1)
                {
                    input = input.Remove(input.IndexOf('\\'), numberOfItemsToRemove);
                    EscapedCharsAreValid(input);
                    return true;
                }
            }

            return false;
        }

        private static bool UnicodeCharsAreValid(string input)
        {
            if (!input.Contains("\\u"))
            {
                return true;
            }

            const int minimumLengthOfUnicodeWithLastQuote = 6;
            return input.Substring(input.IndexOf("\\u") + 1).Length >= minimumLengthOfUnicodeWithLastQuote;
        }
    }
}