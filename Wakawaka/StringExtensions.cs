using System;
using System.Linq;
using System.Text;

namespace Wakawaka
{
    /// <summary>
    /// Provides additional functionality for the <see cref="String"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string where runs of white-space characters are 
        /// replaced by a single character.
        /// </summary>
        /// <param name="value">The string to squeeze.</param>
        /// <returns>A new string where runs of white-space characters are 
        /// replaced with a single character.</returns>
        public static string Squeeze(this string value)
        {
            return Squeeze(value, ' ', '\t', '\n', '\r');
        }

        /// <summary>
        /// Returns a new string where runs of the same character that occur in
        /// <paramref name="chars"/> are replaced by a single character.
        /// </summary>
        /// <param name="value">The string to squeeze.</param>
        /// <param name="chars">A set of characters of which runs are replaced 
        /// by a single character. Specify <c>null</c> to remove all runs of 
        /// identical characters.</param>
        /// <returns>A new string where runs of the same character are replaced
        /// with a single character.</returns>
        public static string Squeeze(this string value, params char[] chars)
        {
            if (value == null) return null;
            if (value.Length == 0) return string.Empty;

            var builder = new StringBuilder(value.Length);

            builder.Append(value[0]);
            for (int i = 1; i < value.Length; i++)
            {
                var c = value[i];
                var inSet = (chars == null || chars.Contains(c));

                if (c != value[i - 1] || !inSet)
                    builder.Append(c);
            }

            return builder.ToString();   
        }

        /// <summary>
        /// Returns a new string with all characters in <paramref name="chars"/>
        /// removed.
        /// </summary>
        /// <param name="value">The string to remove characters from.</param>
        /// <param name="chars">A set of characters to remove from the string.
        /// </param>
        /// <returns>A new string with characters in <paramref name="chars"/>
        /// removed.</returns>
        public static string Delete(this string value, params char[] chars)
        {
            if (value == null) return null;
            if (value.Length == 0) return string.Empty;

            var builder = new StringBuilder(value.Length);
            foreach (var c in value)
            {
                if (!chars.Contains(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a new string with newlines and runs of white-space
        /// characters removed, and white-space at the beginning and end of the
        /// string removed.
        /// </summary>
        /// <param name="value">The string to compact.</param>
        /// <returns>The compacted string.</returns>
        public static string Compact(this string value)
        {
            return value.Trim().Delete('\n', '\r').Squeeze();
        }
    }
}
