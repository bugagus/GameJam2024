using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    private string _code = "";
    private char[] _codeArray;
    private int _currentLetter;
    [SerializeField][Range(1,10)] private int _numberOfLetters;
    private MorseCodeGenerator _morseGenerator;

    [SerializeField] private TMP_Text _goblinText;
    [SerializeField] private TMP_Text _bigText;

    private Goblin _goblin;

    void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
        _goblin = GetComponent<Goblin>();
    }

    void OnEnable()
    {
        _code = _morseGenerator.WordToMorse(_morseGenerator.GetRandomWord(_numberOfLetters));
        _codeArray = _code.ToCharArray();
        _currentLetter = 0;
        PrintGoblinText();
        PrintGlobalText();
    }

    public char GetCurrentLetter()
    {
        return  _codeArray[_currentLetter];
    }

    public void NextLetter()
    {
        Debug.Log("AAAAAAAAAAAAAAA");
        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES AL HABER ACERTADO
        ColorCorrectLetter();

        _currentLetter++;
        if (_currentLetter >= _code.Length - 1)
        {
            _goblin.HasBeenServed();
        }

    }

    private void ColorCorrectLetter()
    {
        _bigText.ForceMeshUpdate();
        Mesh mesh = _bigText.mesh;
        Color[] colors = mesh.colors;

        TMP_CharacterInfo c = _bigText.textInfo.characterInfo[_currentLetter];
        int index = c.index;

        colors[index] = Color.red;
        colors[index+1] = Color.red;
        colors[index+2] = Color.red;
        colors[index+3] = Color.red;

        mesh.colors = colors;

        _bigText.canvasRenderer.SetMesh(mesh);
    }

    public void ResetWord()
    {
        _currentLetter = 0;

        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES PORQUE SE HA EQUIVOCADO
    }
    
    private void PrintGoblinText()
    {
        _goblinText.text = _code;
    }

    public void PrintGlobalText()
    {
        _bigText.text = _code;
    }
}
