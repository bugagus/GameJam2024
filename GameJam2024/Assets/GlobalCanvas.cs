using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GlobalCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points, combo, timer;
    [SerializeField] private TMP_Text scoreF;
    [SerializeField] private TMP_Text comboF;
    [SerializeField] private TMP_Text gradeF;
    [SerializeField] private GameObject FinalScreen, win, gameOver;
    
    public void SetPoints(int p) => points.text = p.ToString();

    public void SetCombo(double c) => combo.text = c.ToString();

    public void SetTimer(float t) => timer.text = ((int)t).ToString();

    public void ShowResultsScreen(int score, int maxCombo, Grade grade)
    {
        FinalScreen.SetActive(true);
        win.SetActive(true);
        scoreF.text = score.ToString();
        comboF.text = maxCombo.ToString();
        gradeF.text = grade.ToString();
    }

    public void ShowGameOver(int score, int maxCombo, Grade grade)
    {
        FinalScreen.SetActive(true);
        gameOver.SetActive(true);
        scoreF.text = score.ToString();
        comboF.text = maxCombo.ToString();
        gradeF.text = grade.ToString();
    }

    public void ReturnToSelector()
    {
        SceneManager.LoadScene("LevelSelector");
        Time.timeScale = 1f;
    }
}
