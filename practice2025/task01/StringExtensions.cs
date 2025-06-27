using System;
using System.Linq;

namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (input == "" || input == null) { return false; }

        string cleaned = "";
        string reversed = "";

        foreach (char c in input.ToLower())
        {
            if (!char.IsWhiteSpace(c) && !char.IsPunctuation(c))
            {
                cleaned += c;
                reversed = c + reversed;
            }
        }

        return cleaned == reversed;
    }
}