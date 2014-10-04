using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindromos
{
    /// <summary>
    /// Static class, doesn't need to be instanciated.
    /// </summary>
    internal static class Checker
    {
        /// <summary>
        /// Codegolf version
        /// http://en.wikipedia.org/wiki/Code_golf
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        int p(String s){return(s.Length<=1)?1:(s[0]==s[s.Length-1]?p(s.Substring(1,s.Length-2)):0);}

        /// <summary>
        /// Method to check if a entered text is palindrome.
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>True if input is palindrome. False otherwise.</returns>
        public static bool IsPalindrome(String text)
        {
            if (text.Length <= 1)
            {
                return true;
            }
            else
            {
                char firstChar = text[0];
                char lastChar = text[text.Length - 1];
                if (firstChar == lastChar)
                {
                    return IsPalindrome(MiddleSubString(text));
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes first and last chars.
        /// </summary>
        /// <param name="fullString">Input string.</param>
        /// <returns>Input string minus first and last char. (Can be a empty string).</returns>
        private static String MiddleSubString(String fullString)
        {
            return fullString.Substring(1, fullString.Length - 2);
        }
    }
}
