using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting.FullSerializer;
using System.Linq;

public class TextWobble : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    bool _fail;

    [SerializeField] private bool wobbleByWord;
    [SerializeField][Range(0, 10)] private float xOffset;
    [SerializeField][Range(0, 10)] private float yOffset;
    [SerializeField] private Gradient color;

    private List<Color> _colors;
    private int _currentLetter;

    // Start is called before the first frame update
    void Start()
    {
        _fail = false;
        _currentLetter = -1;
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);

        _colors = new List<Color>(100);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        Color[] colors = mesh.colors;
        if (wobbleByWord)
        {
            WobbleByWord(colors);
        }
        else
        {
            WobbleByChar(colors);
        }

        mesh.colors = colors;

        mesh.vertices = vertices;
        // 
        //textMesh.canvasRenderer.SetMesh(mesh);
        textMesh.UpdateGeometry(mesh, 0);
    }

    private void WobbleByWord(Color[] colors)
    {
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;

            colors[index] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
            colors[index + 1] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
            colors[index + 2] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
            colors[index + 3] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;



        }
    }

    private void WobbleByChar(Color[] colors)
    {
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;

            // colors[index] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
            // colors[index + 1] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
            // colors[index + 2] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
            // colors[index + 3] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

            if (_fail)
            {
                colors[index] = _colors[index]; //<----AQUI DA UN PUTO ERROR RAFA
                colors[index + 1] = _colors[index + 1];
                colors[index + 2] = _colors[index + 2];
                colors[index + 3] = _colors[index + 3];
            }
            else if (i > _currentLetter) continue;
            else
            {
                colors[index] = _colors[index];
                colors[index + 1] = _colors[index + 1];
                colors[index + 2] = _colors[index + 2];
                colors[index + 3] = _colors[index + 3];
            }
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * xOffset), Mathf.Cos(time * yOffset));
    }

    public void SetColors(Color[] colors, int currentLetter)
    {
        _colors = colors.ToList();
        _currentLetter = currentLetter;
    }

    public void SetWordComplete(bool state)
    {
        wobbleByWord = state;
    }

    public void Reset()
    {
        SetWordComplete(false);
        // for (int i = 0; i < _colors.Count; i++)
        // {
        //     _colors[i] = Color.black;
        // }

        _currentLetter = -1;
        _fail = false;
    }

    public void AllRed()
    {
        for (int i = 0; i < _colors.Count; i++)
        {
            _colors[i] = Color.red;
        }
        _fail = true;
    }
}
