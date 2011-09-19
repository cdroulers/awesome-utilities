using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
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
            else
            {
                return self;
            }
        }

        /// <summary>
        ///     Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and ends at the specified index
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <returns></returns>
        public static string SubstringStartEnd(this string self, int startIndex, int endIndex)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }
            return self.Substring(startIndex, endIndex - startIndex + 1);
        }
    }
}
