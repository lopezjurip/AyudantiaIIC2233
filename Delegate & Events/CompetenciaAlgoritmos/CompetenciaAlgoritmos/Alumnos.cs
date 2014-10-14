using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenciaAlgoritmos
{
    class Pedro : Alumno
    {
        protected override string MiAlgoritmo(string input)
        {
            // Fuente: http://codereview.stackexchange.com/a/43574

            int rightIndex = 0, leftIndex = 0;
            var x = "";
            string currentPalindrome = string.Empty;
            string longestPalindrome = string.Empty;
            for (int currentIndex = 1; currentIndex < input.Length - 1; currentIndex++)
            {
                leftIndex = currentIndex - 1;
                rightIndex = currentIndex + 1;
                while (leftIndex >= 0 && rightIndex < input.Length)
                {
                    if (input[leftIndex] != input[rightIndex])
                    {
                        break;
                    }
                    currentPalindrome = input.Substring(leftIndex, rightIndex - leftIndex + 1);
                    if (currentPalindrome.Length > x.Length)
                        x = currentPalindrome;
                    leftIndex--;
                    rightIndex++;
                }
            }
            return x;
        }

        public override string ToString()
        {
            return "Pedro";
        }
    }

    class Juan : Alumno
    {
        protected override string MiAlgoritmo(string input)
        {
            // Fuente: http://codereview.stackexchange.com/q/43571 

            int rightIndex = 0, leftIndex = 0;
            List<string> paliList = new List<string>();
            string currentPalindrome = string.Empty;
            string longestPalindrome = string.Empty;
            for (int currentIndex = 1; currentIndex < input.Length - 1; currentIndex++)
            {
                leftIndex = currentIndex - 1;
                rightIndex = currentIndex + 1;
                while (leftIndex >= 0 && rightIndex < input.Length)
                {
                    if (input[leftIndex] != input[rightIndex])
                    {
                        break;
                    }
                    currentPalindrome = input.Substring(leftIndex, rightIndex - leftIndex + 1);
                    paliList.Add(currentPalindrome);
                    leftIndex--;
                    rightIndex++;
                }
            }
            var x = (from c in paliList
                     select c).OrderByDescending(w => w.Length).First();
            return x; 
        }

        public override string ToString()
        {
            return "Juan";
        }
    }

    class Diego : Alumno
    {
        protected override string MiAlgoritmo(string input)
        {
            // Fuente: http://strivingandlearning.blogspot.com/2013/06/find-longest-palindrome-in-given-string.html

            int stringLength = input.Length;
            int maxPalindromeStringLength = 0;
            int maxPalindromeStringStartIndex = 0;
            for (int i = 0; i < stringLength; i++)
            {
                int currentCharIndex = i;
                for (int lastCharIndex = stringLength - 1; lastCharIndex > currentCharIndex; lastCharIndex--)
                {
                    if (lastCharIndex - currentCharIndex + 1 < maxPalindromeStringLength)
                    {
                        break;
                    }
                    bool isPalindrome = true;
                    if (input[currentCharIndex] != input[lastCharIndex])
                    {
                        continue;
                    }
                    else
                    {
                        int matchedCharIndexFromEnd = lastCharIndex - 1;
                        for (int nextCharIndex = currentCharIndex + 1; nextCharIndex < matchedCharIndexFromEnd; nextCharIndex++)
                        {
                            if (input[nextCharIndex] != input[matchedCharIndexFromEnd])
                            {
                                isPalindrome = false;
                                break;
                            }
                            matchedCharIndexFromEnd--;
                        }
                    }
                    if (isPalindrome)
                    {
                        if (lastCharIndex + 1 - currentCharIndex > maxPalindromeStringLength)
                        {
                            maxPalindromeStringStartIndex = currentCharIndex;
                            maxPalindromeStringLength = lastCharIndex + 1 - currentCharIndex;
                        }
                        break;
                    }
                }
            }
            if (maxPalindromeStringLength > 0)
            {
                return input.Substring(maxPalindromeStringStartIndex, maxPalindromeStringLength);
            }
            return null;
        }

        public override string ToString()
        {
            return "Diego";
        }
    }
}
