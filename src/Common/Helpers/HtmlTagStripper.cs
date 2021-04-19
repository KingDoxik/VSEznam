using System;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    /// <summary>
    /// Helper class for HTML tag stripping
    /// </summary>
    public static class HtmlTagStripper
    {
        /// <summary>
        /// Removes all html tags from string using Regex
        /// </summary>
        /// <param name="html">Html string that will be stripped</param>
        /// <returns>string without html tags</returns>
        public static string StripHtmlTags(this string html)
        {
            return Regex.Replace(html, "<.*?>", String.Empty);
        }
    }
}
