using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoblinTimer : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private Slider slider;

    private void Update()
    {
        slider.value += 1f/timer*Time.deltaTime;
        if(slider.value == 1)
        {
            GoAway();
        }
    }

    private void GoAway()
    {
        GetComponent<Goblin>().GoAway();
    }
}
