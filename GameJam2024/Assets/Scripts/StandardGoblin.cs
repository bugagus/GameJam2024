using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandardGoblin : Goblin
{
    private int _nLetters = 2;
    private string _phrase;
    private float timer;

    private new void Update()
    {
        timer-=Time.deltaTime;
        if(timer <= 0)
        {
            GoAway();
        }
    }

    private new void OnEnable()
    {
        _phrase = _morseGenerator.GetRandomWord(_nLetters);
        timer = 30f;
        PrintMorse();
        base.OnEnable();
    }

    private void PrintMorse()
    {
        textMorse.text = _phrase;
    }
}
