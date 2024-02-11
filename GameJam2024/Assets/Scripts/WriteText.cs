using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WriteText : MonoBehaviour
{
    public GameObject TextPanel;
    public TMP_Text toWriteText;
    [SerializeField] string[] texts;
    private int textNumber;
    public bool writing;
    public float timeBetweenLetters = 0.09f;
    public int timeToEraseText = 4;

    private void Start()
    {
        textNumber = 0;
        DOVirtual.DelayedCall(2.0f, ()=>{StartToWrite(texts[0]);}, false);
    }

    public void StartToWrite(string text)
    {
        StartCoroutine(WriteTexts(text));
    }

    public IEnumerator WriteTexts(string text)
    {
        if (!writing)
        {
            toWriteText.text = "";
            writing = true;
            TextPanel.SetActive(true);

            foreach (char letter in text)
            {
                toWriteText.text = toWriteText.text + letter;
                yield return new WaitForSeconds(timeBetweenLetters * Time.timeScale);
            }

            yield return new WaitForSecondsRealtime(timeToEraseText);
            toWriteText.text = "";

            TextPanel.SetActive(false);
            writing = false;
            textNumber++;
            if(textNumber == texts.Length) SceneManager.LoadScene("LevelSelector");
            StartToWrite(texts[textNumber]);
        }
        yield return null;
    }

    
}
