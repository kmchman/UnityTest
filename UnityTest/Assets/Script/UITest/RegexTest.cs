using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public struct BadWord
{
    public BadWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) throw new ArgumentNullException(nameof(word));

        int startIndex = 0;
        int length = word.Length;

        // Skip leading / trailing white-space:
        while (length > 0 && char.IsWhiteSpace(word[startIndex]))
        {
            startIndex++;
            length--;
        }
        while (length > 0 && char.IsWhiteSpace(word[startIndex + length - 1]))
        {
            length--;
        }

        // If the word ends with "!", then it's a case-sensitive match:
        if (length > 0 && word[startIndex + length - 1] == '!')
        {
            CaseSensitive = true;
            length--;
        }
        else
        {
            CaseSensitive = false;
        }

        // If the word ends with "*", filter anything starting with the word:
        if (length > 0 && word[startIndex + length - 1] == '*')
        {
            Suffix = "(?=\\w*\\b)";
            length--;
        }
        else
        {
            Suffix = "\\b";
        }

        // If the word starts with "*", filter anything ending with the word:
        if (length > 0 && word[startIndex] == '*')
        {
            Prefix = "(?<=\\b\\w*)";
            startIndex++;
            length--;
        }
        else
        {
            Prefix = "\\b";
        }

        Word = length != 0 ? word.Substring(startIndex, length) : null;
    }

    public string Word { get; }
    public string Prefix { get; }
    public string Suffix { get; }
    public bool CaseSensitive { get; }

    public Regex ToRegularExpression()
    {
        if (string.IsNullOrWhiteSpace(Word)) return null;

        string pattern = Prefix + Regex.Escape(Word) + Suffix;
        var options = CaseSensitive ? RegexOptions.ExplicitCapture : RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase;
        return new Regex(pattern, options);
    }
}



public sealed class WordReplacement
{
    public WordReplacement(BadWord word, string replacement)
    {
        if (string.IsNullOrWhiteSpace(word.Word)) throw new ArgumentNullException(nameof(word));

        Pattern = word.ToRegularExpression();
        CaseSensitive = word.CaseSensitive;
        Replacement = replacement;

        if (CaseSensitive || replacement == null || replacement.Any(char.IsUpper))
        {
            Replacer = (Match m) => Replacement;
        }
        else
        {
            Replacer = (Match m) => MatchCase(m.Value, Replacement);
        }
    }

    public WordReplacement(string word, string replacement) : this(new BadWord(word), replacement)
    {
    }

    public Regex Pattern { get; }
    public string Replacement { get; }
    public bool CaseSensitive { get; }
    public MatchEvaluator Replacer { get; }

    public static string MatchCase(string wordToReplace, string replacement)
    {
        if (null == replacement) return string.Empty;
        if (wordToReplace.All(char.IsLower)) return replacement;
        if (wordToReplace.All(char.IsUpper)) return replacement.ToUpperInvariant();

        char[] result = replacement.ToCharArray();
        bool changed = false;

        if (wordToReplace.Length == replacement.Length)
        {
            for (int index = 0; index < result.Length; index++)
            {
                if (char.IsUpper(wordToReplace[index]))
                {
                    char c = result[index];
                    result[index] = char.ToUpperInvariant(c);
                    if (result[index] != c) changed = true;
                }
            }
        }
        else
        {
            if (char.IsUpper(wordToReplace[0]))
            {
                char c = result[0];
                result[0] = char.ToUpperInvariant(c);
                if (result[0] != c) changed = true;
            }
            if (char.IsUpper(wordToReplace[wordToReplace.Length - 1]))
            {
                int index = result.Length - 1;
                char c = result[index];
                result[index] = char.ToUpperInvariant(c);
                if (result[index] != c) changed = true;
            }
        }

        return changed ? new string(result) : replacement;
    }

    public string Replace(string input) => Pattern.Replace(input, Replacer);
}



public sealed class Clbuttifier2000
{
    public Clbuttifier2000(IEnumerable<KeyValuePair<string, string>> replacements)
    {
        Replacements = replacements.Select(p => new WordReplacement(p.Key, p.Value)).ToList().AsReadOnly();
    }

    public IReadOnlyList<WordReplacement> Replacements { get; }

    public string Clbuttify(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            foreach (var replacement in Replacements)
            {
                message = replacement.Replace(message);
            }
        }

        return message;
    }
}
