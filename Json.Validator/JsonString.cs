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
            const int firstValidCharForJsonString = 32;
            for (int i = 0; i < input.Length; i++)
            {
                if (Convert.ToChar(input[i]) < firstValidCharForJsonString)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsValidChars(string input)
        {
            return !ContainsControlCharacters(input)
                && !ContainsExceptedChars(input)
                && HasValidUnicodeChars(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool IsDoubleQuoted(string input)
        {
            return input.Length >= 2 && input[0] == '"' && input[input.Length - 1] == '"';
        }

        private static bool ContainsExceptedChars(string input)
        {
            if (!input.Contains('\\'))
            {
                return false;
            }

            return !EscapedCharsAreValid(input);
        }

        private static bool EscapedCharsAreValid(string input)
        {
            const byte numberOfItemsToRemove = 2;
            char[] allowedToBeEscaped = { '"', '\\', '/', 'b', 'f', 'n', 'r', 't', 'u' };
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

        private static bool HasValidUnicodeChars(string input)
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