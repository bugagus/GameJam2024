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
    private Animator animator;
    [SerializeField][Range(1, 10)] private int _numberOfLetters;
    private MorseCodeGenerator _morseGenerator;
    [SerializeField] private TMP_Text _goblinText;
    [SerializeField] private TMP_Text _bigText;
    [SerializeField] private TextWobble _bigTextColorScript;

    private Goblin _goblin;

    private Color[] colors;

    [SerializeField] private Color failColor;
    [SerializeField] private Color okColor;

    void Awake()
    {
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
        _goblin = GetComponent<Goblin>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        _code = _morseGenerator.WordToMorse(_morseGenerator.GetRandomWord(_numberOfLetters));
        _codeArray = _code.ToCharArray();
        _currentLetter = -1;
        PrintGoblinText();
    }

    public char GetCurrentLetter()
    {
        // La primera letra no se debe pintar al iniciar por lo que la primera letra empieza en -1
        return _codeArray[_currentLetter + 1];
    }

    public void NextLetter()
    {
        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES AL HABER ACERTADO


        _currentLetter++;
        ColorCorrectLetter();
        if (_currentLetter >= _code.Length - 1)
        {
            _goblin.HasBeenServed();
            animator.SetTrigger("DisappearText");
            _bigTextColorScript.SetWordComplete(true);
        }

    }

    private void ColorCorrectLetter()
    {
        if (_currentLetter < 0) return;

        int vertexIndex = _bigText.textInfo.characterInfo[_currentLetter].vertexIndex;

        colors[vertexIndex] = okColor;
        colors[vertexIndex + 1] = okColor;
        colors[vertexIndex + 2] = okColor;
        colors[vertexIndex + 3] = okColor;

        _bigTextColorScript.SetColors(colors, _currentLetter);
    }

    public void ResetWord()
    {
        if (_currentLetter < 0) return;
        //AQUI IRIA LA LLAMADA AL TEXTO PARA QUE CAMBIE DE COLOR Y/O HAGA ANIMACIONES PORQUE SE HA EQUIVOCADO
        int vertexIndex = _bigText.textInfo.characterInfo[_currentLetter].vertexIndex;

        for (int i = 0; i < vertexIndex + 4; i++)
        {
            colors[i] = failColor;
        }
        _bigTextColorScript.SetColors(colors, _currentLetter);
        _currentLetter = -1;
    }

    private void PrintGoblinText()
    {
        _goblinText.text = _code;
    }

    public void PrintGlobalText()
    {
        _bigText.text = _code;
    }

    private void OnTriggerEnter(Collider a)
    {
        if (a.CompareTag("PrimeroFila"))
        {
            FindObjectOfType<InputManager>().SetNextGoblin(this);
            animator.SetTrigger("AppearText");
            PrintGlobalText();
            _bigTextColorScript.Reset();
            _bigText.ForceMeshUpdate();
            colors = _bigText.mesh.colors;
        }
    }
}
