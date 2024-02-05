using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    private string _code = "";

    private MorseCodeGenerator _morseGenerator;

    private int _numberOfLetters;

    void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
        
        _numberOfLetters = GetComponent<Goblin>().GetNumberOfLetters();
    }

    void OnEnable()
    {
        _code = _morseGenerator.WordToMorse(_morseGenerator.GetRandomWord(_numberOfLetters));
    }

    
}
