using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points, combo, timer;

    public void SetPoints(int p) => points.text = p.ToString();

    public void SetCombo(double c) => combo.text = c.ToString();

    public void SetTimer(float t) => timer.text = ((int)t).ToString();
}
