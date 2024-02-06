using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    private string _code = "";
    private char[] _codeArray;
    private int _currentLetter;
    [SerializeField][Range(1, 10)] private int _numberOfLetters;
    private MorseCodeGenerator _morseGenerator;
    [SerializeField] private TMP_Text _goblinText;
    [SerializeField] private TMP_Text _bigText;
    [SerializeField] private TextWobble _bigTextColorScript;

    private Goblin _goblin;

    private Color[] colors;

    void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
        _goblin = GetComponent<Goblin>();
    }

    void OnEnable()
    {
        _code = _morseGenerator.WordToMorse(_morseGenerator.GetRandomWord(_numberOfLetters));
        _codeArray = _code.ToCharArray();
        _currentLetter = -1;
        _bigText.ForceMeshUpdate();
        colors = _bigText.mesh.colors;
        PrintGoblinText();
    }

    private void Update()
    {
    }

    public char GetCurrentLetter()
    {
        // La primera letra no se debe pintar al iniciar por lo que la primera letra empieza en -1
        return _codeArray[_currentLetter + 1];
    }

    public void NextLetter()
    {
        Debug.Log("AAAAAAAAAAAAAAA");
        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES AL HABER ACERTADO
        

        _currentLetter++;
        ColorCorrectLetter();
        if (_currentLetter >= _code.Length - 1)
        {
            _goblin.HasBeenServed();
            _bigTextColorScript.SetWordComplete(true);
        }

    }

    private void ColorCorrectLetter()
    {
        if (_currentLetter < 0) return;

        int vertexIndex = _bigText.textInfo.characterInfo[_currentLetter].vertexIndex;

        colors[vertexIndex] = Color.green;
        colors[vertexIndex + 1] = Color.green;
        colors[vertexIndex + 2] = Color.green;
        colors[vertexIndex + 3] = Color.green;

        _bigTextColorScript.SetColors(colors, _currentLetter);
    }

    public void ResetWord()
    {
        if (_currentLetter < 0) return;
        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES PORQUE SE HA EQUIVOCADO
        int vertexIndex = _bigText.textInfo.characterInfo[_currentLetter].vertexIndex;

        for (int i = 0; i < vertexIndex + 4; i++)
        {
            colors[i] = Color.red;
        }

        _currentLetter = -1;
    }

    private void PrintGoblinText()
    {
        _goblinText.text = _code;
    }

    public void PrintGlobalText()
    {
        Debug.Log("Printeo, soy el goblin " + this.gameObject);
        _bigText.text = _code;
    }

    private void OnTriggerEnter(Collider a)
    {
        if(a.CompareTag("PrimeroFila"))
        {
            Debug.Log("HOlaaaa");
            PrintGlobalText();
        }
    }
}
