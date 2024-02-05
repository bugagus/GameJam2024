using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandardGoblin : Goblin
{
    private int _nLetters = 2;
    private string _phrase;
    private float timer;

    private new void OnEnable()
    {
        _phrase = _morseGenerator.GetRandomWord(_nLetters);
        PrintMorse();
        base.OnEnable();
    }

    private void PrintMorse()
    {
        textMorse.text = _phrase;
    }
}
