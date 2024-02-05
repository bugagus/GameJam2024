using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoblinMorse : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMorse;
    private MorseCodeGenerator _morseGenerator;
    private int _nLetters = 2;
    private string _phrase;
    // Start is called before the first frame update
    private void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
    }
    
    private void OnEnable()
    {
        _phrase = _morseGenerator.GetRandomWord(_nLetters);
        PrintMorse();
    }
    
    private void PrintMorse()
    {
        textMorse.text = _phrase;
    }

    public char CurrentLetter()
    {
        return 'E';
    }

    public void NextLetter()
    {
    }

    public void ResetWord()
    {
    }
    
}
