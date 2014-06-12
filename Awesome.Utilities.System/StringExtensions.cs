using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    ///     String extensions!
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates the string to a maximum length
        /// </summary>
        /// <param name="self">The string to truncate</param>
        /// <param name="maximumLength">The maximum number of characters to keep</param>
        /// <param name="suffix">The string to put at the end of the truncated string</param>
        /// <param name="trimSpace">if set to <c>true</c>, trim the spaces before the suffix.</param>
        /// <returns>A truncated string</returns>
        public static string Truncate(this string self, int maximumLength, string suffix = null, bool trimSpace = true)
        {
            Validate.Is.HigherThanOrEqualTo(maximumLength, 0, "maximumLength");
            suffix = suffix ?? string.Empty;

            int subStringLength = maximumLength - suffix.Length;

            Validate.Is.HigherThan(subStringLength, 0, "subStringLength");

            if (self != null && self.Length > maximumLength)
            {
                string truncatedString = self.Substring(0, subStringLength);

                // incase the last character is a space
                if (trimSpace)
                {
                    truncatedString = truncatedString.TrimEnd();
                    truncatedString += suffix;
                }

                if (!trimSpace && !truncatedString.EndsWith(" "))
                {
                    truncatedString += suffix;
                }

                return truncatedString;
            }

                return self;
        }

        /// <summary>
        ///     Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and ends at the specified index
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <returns>A substring from startIndex to endIndex</returns>
        public static string SubstringStartEnd(this string self, int startIndex, int endIndex)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }

            return self.Substring(startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// Removes the diacritics from the string.
        /// </summary>
        /// <param name="self">The string.</param>
        /// <returns>A string without diacritics.</returns>
        public static string RemoveDiacritics(this string self)
        {
            string normalized = self.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char t in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(t) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Removes the HTML from the string. will return funky results for invalid markup.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>A string without any HTML.</returns>
        public static string RemoveHtml(this string self)
        {
            var array = new char[self.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < self.Length; i++)
            {
                char let = self[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Returns a slug of the specified string.
        /// </summary>
        /// <param name="self">The phrase.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns>A slug of the string</returns>
        public static string ToSlug(this string self, int maxLength = 50)
        {
            string str = self.ToLower().RemoveDiacritics();

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", string.Empty);

            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();

            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();

            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }
    }
}
