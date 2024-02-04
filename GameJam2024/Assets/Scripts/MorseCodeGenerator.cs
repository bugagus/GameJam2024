using System.Collections.Generic;
using UnityEngine;

public class MorseCodeGenerator : MonoBehaviour
{
    private Dictionary<char, string> morseCodes = 
        new Dictionary<char, string>() {
            {'A', "·-"   }, {'B', "-···" }, {'C', "-·-·" }, {'D', "-··"  },
            {'E', "·"    }, {'F', "··-·" }, {'G', "--·"  }, {'H', "····" },
            {'I', "··"   }, {'J', "·---" }, {'K', "-·-"  }, {'L', "·-··" },
            {'M', "--"   }, {'N', "-·"   }, {'O', "---"  }, {'P', "·--·" },
            {'Q', "--·-" }, {'R', "·-·"  }, {'S', "···"  }, {'T', "-"    },
            {'U', "··-"  }, {'V', "···-" }, {'W', "·--"  }, {'X', "-··-" },
            {'Y', "-·--" }, {'Z', "--··" }, {'1', "·----"}, {'2', "··---"},
            {'3', "···--"}, {'4', "····-"}, {'5', "·····"}, {'6', "-····"},
            {'7', "--···"}, {'8', "---··"}, {'9', "----·"}, {'0', "-----"}
        };

    private string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    private string LineToMorse(string line)
    {
        string[] words = line.Split(' ');
        string result = "";
        foreach (string word in words)
        {
            result += WordToMorse(word) + " ";
        }
        return result;
    }

    private string WordToMorse(string word)
    {
        string result = "";

        foreach (char elem in word)
        {
            char key = elem;

            if (!morseCodes.ContainsKey(key))
                result += elem;
            else
                result += morseCodes[key];
        }

        return result;
    }

    public string GetRandomWord(int numberOfLetters)
    {
        string result = "";

        for (int i = 0; i < numberOfLetters; i++)
        {
            int index = Random.Range(0, symbols.Length);
            result += symbols[index];
        }

        return result;
    }
}
