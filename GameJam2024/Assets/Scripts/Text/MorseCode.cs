using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    private string _code = "";
    private char[] _codeArray;
    private int _currentLetter;
    [SerializeField] private int _numberOfLetters;

    private MorseCodeGenerator _morseGenerator;


    void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
    }

    void OnEnable()
    {
        _code = _morseGenerator.WordToMorse(_morseGenerator.GetRandomWord(_numberOfLetters));
        _codeArray = _code.ToCharArray();
        _currentLetter = 0;
    }

    public char GetCurrentLetter()
    {
        return  _codeArray[_currentLetter];
    }

    public void NextLetter()
    {
        _currentLetter++;
        if(_currentLetter >= _numberOfLetters-1)
        {
            GetComponent<Goblin>().HasBeenServed();
        }

        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES AL HABER ACERTADO
    }

    public void ResetWord()
    {
        _currentLetter = 0;

        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES PORQUE SE HA EQUIVOCADO
    }
    
}
