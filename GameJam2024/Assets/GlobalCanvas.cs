using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points, combo, timer;
    [SerializeField] private TMP_Text scoreF;
    [SerializeField] private TMP_Text comboF;
    [SerializeField] private TMP_Text gradeF;
    [SerializeField] private GameObject FinalScreen;
    private ScoreManager _scoreManager;

    public void SetPoints(int p) => points.text = p.ToString();

    public void SetCombo(double c) => combo.text = c.ToString();

    public void SetTimer(float t) => timer.text = ((int)t).ToString();

    public void FinishGame()
    {
        FinalScreen.SetActive(true);
    }

    public void SetPointsF(int p) => scoreF.text = p.ToString();

    public void SetComboF(int c) => comboF.text = c.ToString();

    public void SetGradeF(Grade g) => gradeF.text = g.ToString();
}
