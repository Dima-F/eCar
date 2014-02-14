﻿using System.Text.RegularExpressions;

namespace eCar.Applicaton.Infrastructure
{
    public static class StringExtensions
    {
        /// <summary>
        /// Null if the string is empty, otherwise the original string.
        /// (Useful to use with with null coalesce, e.g. myString.AsNullIfEmpty() ?? defaultString
        /// </summary>
        public static string AsNullIfEmpty(this string items)
        {
            return string.IsNullOrEmpty(items) ? null : items;
        }

        /// <summary>
        /// Null if the string is empty or whitespace, otherwise the original string.
        /// (Useful to use with with null coalesce, e.g. myString.AsNullIfWhiteSpace() ?? defaultString
        /// </summary>
        public static string AsNullIfWhiteSpace(this string items)
        {
            return string.IsNullOrWhiteSpace(items) ? null : items;
        }


        /// <summary>
        /// Creates a URL friendly slug from a string
        /// </summary>
        public static string ToUrlSlug(this string str)
        {
            string originalValue = str;

            // Repalce any characters that are not alphanumeric with hypen
            str = Regex.Replace(str, "[^a-z^0-9]", "-", RegexOptions.IgnoreCase);

            // Replace all double hypens with single hypen
            string pattern = "--";
            while (Regex.IsMatch(str, pattern))
                str = Regex.Replace(str, pattern, "-", RegexOptions.IgnoreCase);

            // Remove leading and trailing hypens ("-")
            pattern = "^-|-$";
            str = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);

            return str.ToLower();
        }
    }
}