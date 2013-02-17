using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NetDog
{
    public static class Utility
    {
        /// <summary>
        ///  Returns the first occurrence of the regular expression specified in the 
        ///  System.Text.RegularExpressions.Regex constructor in the specified input string
        /// </summary>
        public static string firstMatch(this Regex regex, string input)
        {
            Match match = regex.Match(input);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }

        /// <summary>
        ///  Retrieves a substring from this instance. The substring lies between two search strings.
        /// </summary>
        public static string Substring(this string target, string start, string end)
        {
            int startPosition = target.IndexOf(start) + start.Length;
            int endPosition = target.IndexOf(end, startPosition);

            if (endPosition == -1)
            {
                return "";
            }

            return target.Substring(startPosition, endPosition - startPosition);
        }

        /// <summary>
        ///  Returns true if the Array is empty.
        /// </summary>
        public static bool Empty(this byte[] data)
        {
            for (long i = 0; i < data.LongLength; i++)
            {
                if (data[i] != 0)
                    return false;
            }
            return true;
        }
    }
}
