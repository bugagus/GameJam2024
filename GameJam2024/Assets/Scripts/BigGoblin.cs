using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGoblin : Goblin
{
    private int _nLetters = 3;
    private string _phrase;

    private new void OnEnable()
    {
        _phrase = _morseGenerator.LineToMorse(_morseGenerator.GetRandomWord(_nLetters));
        PrintMorse();
        base.OnEnable();
    }

    private void PrintMorse()
    {
        textMorse.text = _phrase;
    }
}
