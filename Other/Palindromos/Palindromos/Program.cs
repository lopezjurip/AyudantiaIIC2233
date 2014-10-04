using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Patricio López
namespace Palindromos
{
    class Program
    {
        /// <summary>
        /// Exit keyword.
        /// </summary>
        private const String EXIT = "exit";

        /// <summary>
        /// Message to show.
        /// </summary>
        private const String PALINDROME_OUTPUT_TRUE = "is palindrome.";

        /// <summary>
        /// Message to show.
        /// </summary>
        private const String PALINDROME_OUTPUT_FALSE = "is not palindrome.";

        static void Main(string[] args)
        {
            String input = Console.ReadLine();
            input = input.ToLower();

            while (!input.Equals(EXIT))
            {
                if (Checker.IsPalindrome(input))
                {
                    Console.WriteLine(input + " " +PALINDROME_OUTPUT_TRUE);
                }
                else
                {
                    Console.WriteLine(input + " " + PALINDROME_OUTPUT_FALSE);
                }
                input = Console.ReadLine();
                input = input.ToLower();
            }
        }
    }
}
