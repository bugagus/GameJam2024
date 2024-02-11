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
    public float timeBetweenLetters = 0.5f;
    public int timeToEraseText = 4;

    private void Start()
    {
        textNumber = 0;
        DOVirtual.DelayedCall(2.0f, ()=>{StartToWrite(texts[0]);}, false);
        DOVirtual.DelayedCall(7.7f, ()=>{StartToWrite(texts[1]);}, false);
        DOVirtual.DelayedCall(17.50f, ()=>{StartToWrite(texts[2]);}, false);
        DOVirtual.DelayedCall(34.50f, ()=>{StartToWrite(texts[3]);}, false);
        DOVirtual.DelayedCall(45.50f, ()=>{StartToWrite(texts[4]);}, false);
        DOVirtual.DelayedCall(50.50f, ()=>{SceneManager.LoadScene("LevelSelector");}, false);
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
                yield return new WaitForSeconds(timeBetweenLetters * Time.deltaTime);
            }

            yield return new WaitForSecondsRealtime(timeToEraseText * Time.deltaTime);
            writing = false;
        }
        yield return null;
    }

    
}
