using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColor : MonoBehaviour
{
    TMP_Text textMesh;
 
    Mesh mesh;
 
    Vector3[] vertices;
 
    List<int> wordIndexes;
    List<int> wordLengths;
 
    [SerializeField] private bool wordComplete;
    [SerializeField][Range(0, 10)] private float xOffset;
    [SerializeField][Range(0, 10)] private float yOffset;
    [SerializeField] private Gradient color;
 
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
 
        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();
 
        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
                wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
                wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }
 
    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;

        Color[] colors = mesh.colors;

        if (wordComplete)
            WobbleByWord(colors);
        else
            ColorByChar(colors);
        
        mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private void WobbleByWord(Color[] colors)
    {
        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex;

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
    }

    private void ColorByChar(Color[] colors)
    {
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            colors[index] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
            colors[index + 1] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
            colors[index + 2] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
            colors[index + 3] = color.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));
        }
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*xOffset), Mathf.Cos(time*yOffset));
    }
}