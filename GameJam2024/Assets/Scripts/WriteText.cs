using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WriteText : MonoBehaviour
{
    public GameObject TextPanel;
    public TMP_Text toWriteText;
    public bool writing;
    float timeBetweenLetters = 0.09f;
    int timeToEraseText = 4;

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
        }
        yield return null;
    }

    
}
