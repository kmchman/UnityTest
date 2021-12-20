using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class UITestManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cubeObject;

    public void OnClickBtnMoveCube()
    {
    }

    private void OnEnable()
    {
        //string s1 = "The quick brown fo x jumps over the lazy dog";
        //string s2 = "fox";
        //bool b = s1.Contains(s2);
        //Debug.Log(string.Format("'{0}' is in the string '{1}': {2}", s2, s1, b));
        //if (b)
        //{
        //    int index = s1.IndexOf(s2);
        //    if (index >= 0)
        //        Debug.Log(string.Format("'{0} begins at character position {1}", s2, index + 1));
        //}
        //RegexTest4();
        AESTest();
    }

    private void AESTest()
    {
        string str = "원본 문자 정보";
        Debug.Log("plain : " + str);

        string str1 = AESCrypto.AESEncrypt128(str);
        Debug.Log("AES128 encrypted : " + str1);

        string str2 = AESCrypto.AESDecrypt128(str1);
        Debug.Log("AES128 decrypted : " + str2);
    }

    private void RegexTest()
    {
        string pattern = @"\bdin\w*\b";
        string input = "An extraordinary day dawns with each new day.";
        Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        if (m.Success)
            Debug.Log(string.Format("Found '{0}' at position {1}.", m.Value, m.Index));
    }
    private void RegexTest1()
    {
        string pattern = @"es";
        string newPattern = "";
        foreach (var c in pattern)
        {
            newPattern += c + @"\s*";
        }
        Regex rgx = new Regex(newPattern);
        string sentence = "Who writes the se notes?";

        foreach (Match match in rgx.Matches(sentence))
        {
            Debug.Log(string.Format("Found '{0}' at position {1}", match.Value, match.Index));

        }
    }

    private void RegexTest2()
    {
        string input = "An extraordinary tes  t day dawns with each new day.";
        BadWord test = new BadWord("test");
        Match m = test.ToRegularExpression().Match(input);

        if (m.Success)
            Debug.Log(string.Format("Found '{0}' at position {1}.", m.Value, m.Index));
    }

    private void RegexTest3()
    {
        var filter = new Clbuttifier2000(new Dictionary<string, string>
        {
            ["poop*"] = "p**p",
            ["PHB!"] = "boss",
            ["gotten"] = "become",
        });

        string input = "My PHB has gotten started on his new blog phblog.com. He's SUCH A MISBEGOTTEN Poophead!";
        string expected = "My boss has become started on his new blog phblog.com. He's SUCH A MISBEGOTTEN P**phead!";
        string actual = filter.Clbuttify(input);
        Debug.Assert(actual == expected);
    }

    private void RegexTest4()
    {

        string input = "area bare arena mare";
        string pattern = @"\bare\w*";
        Console.WriteLine("Words that begin with 'are':");
        foreach (Match match in Regex.Matches(input, pattern))
            Debug.Log(string.Format("'{0}' found at position {1}", match.Value, match.Index));
    }

    private void FindSpece()
    {
        string pattern = " ";
        string input = "An extraordinary day dawns with each new day.";
        Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        if (m.Success)
            Debug.Log(string.Format("Found '{0}' at position {1}.", m.Value, m.Index));
    }

    private void EnumerableTest()
    {
        var test = Number();
        foreach (var tmp in test)
        {
            Debug.Log(tmp + " ");
        }
    }

    private IEnumerable Number()
    {
        int num = 0;

        while (true)
        {
            num++;
            yield return num;

            if (num >= 100)
            {
                yield break;
            }
        }
    }
}

