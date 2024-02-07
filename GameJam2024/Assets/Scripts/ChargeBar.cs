using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChargeBar : MonoBehaviour
{
    public Slider sliderMain;
    public Gradient sliderGradientColor;
    public Image sliderFill;
    private InputControls inputControls;
    public float sliderStartingValue;
    public float sliderEndValue;
    public float sliderMinValue;
    private bool _pressingKey;
    //public bool sliderBGFill;

    void Start()
    {
        sliderEndValue = sliderStartingValue * 10f;
        sliderMinValue = 0;
        sliderMain.maxValue = sliderEndValue;
        sliderFill.color = sliderGradientColor.Evaluate(1f);
        inputControls = FindObjectOfType<InputManager>()._input;
        inputControls.Controls.Tick.started += ctx => StartTick(ctx);   
        inputControls.Controls.Tick.canceled += ctx => EndTick(ctx);
    }

    void Update()
    {
        if(_pressingKey)
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, 4f * Time.deltaTime);
        }
        else
        {
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue,sliderMinValue, sliderStartingValue);

        }

        sliderMain.value = sliderStartingValue;

        //if(_pressingKey)
        //{
        //    if(sliderBGFill == false)
        //    {
        //        sliderBGFill = true;
        //    }
        //    else
        //    {
        //        sliderBGFill = false;
        //    }
        //}
        ///if(sliderBGFill)
        ///{
        ///    sliderFill.color = sliderGradientColor.Evaluate(sliderMain.normalizedValue);
        ///    sliderColor.text = "Que esta en gradiante o no se que pollas";
        ///}
        ///else
        ///{
        ///    sliderFill.color = sliderPlainColor.Evaluate(sliderMain.normalizedValue);
        ///    sliderColor.text = "Pone no se que de plain color en el tutorial";
        ///}
    }

    private void StartTick(InputAction.CallbackContext context)
    {
        _pressingKey = true;
    }

    private void EndTick(InputAction.CallbackContext context)
    {
        _pressingKey = false;
    }
}
