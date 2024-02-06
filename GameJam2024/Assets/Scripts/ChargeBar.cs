using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    public Slider sliderMain;
    public TMP_Text sliderCounter;
    public TMP_Text SliderStatus;
    public TMP_Text sliderColor;
    public Gradient sliderGradientColor;
    public Gradient sliderPlainColor;
    public Image sliderFill;

    public float sliderStartingValue;
    public float sliderEndValue;
    public float sliderMinValue;

    public bool sliderBGFill;

    void Start()
    {
        sliderEndValue = sliderStartingValue * 10f;
        sliderMinValue = 0;
        sliderCounter.text = sliderMinValue.ToString();
        sliderMain.maxValue = sliderEndValue;
        sliderFill.color = sliderGradientColor.Evaluate(1f);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, 4f * Time.deltaTime);
            SliderStatus.text = "Sapulsao la E";
        }
        else
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue,sliderMinValue, sliderStartingValue);
            SliderStatus.text = "Se soltó la E";
        }

        sliderMain.value = sliderStartingValue;
        sliderCounter.text = Mathf.RoundToInt(sliderStartingValue).ToString();

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(sliderBGFill == false)
            {
                sliderBGFill = true;
            }
            else
            {
                sliderBGFill = false;
            }
        }

        if(sliderBGFill)
        {
            sliderFill.color = sliderGradientColor.Evaluate(sliderMain.normalizedValue);
            sliderColor.text = "Que esta en gradiante o no se que pollas";
        }
        else
        {
            sliderFill.color = sliderPlainColor.Evaluate(sliderMain.normalizedValue);
            sliderColor.text = "Pone no se que de plain color en el tutorial";
        }
    }
}
