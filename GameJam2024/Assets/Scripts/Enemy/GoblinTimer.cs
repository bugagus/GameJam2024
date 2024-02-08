using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoblinTimer : MonoBehaviour
{
    private GameObject _gameManager;

    [SerializeField] private float timer;
    [SerializeField] private Slider slider;

    private bool _timeStopped;
    private bool _isGoingAway;
    
    private void OnEnable()
    {
        _gameManager = GameObject.Find("GameManager");
        _isGoingAway = false;
        _timeStopped = AbilityManager.timeStop;
    }
    private void Update()
    {
        if(!_timeStopped)
        {
            slider.value += 1f/timer*Time.deltaTime;
            if(slider.value == 1 && !_isGoingAway)
            {
                _gameManager.GetComponent<ScoreManager>().AddGoblinFailed();
                GoAway();
            }
        }
    }

    private void GoAway()
    {
        GetComponent<Goblin>().GoAway();
        _isGoingAway = true;
    }

    public void SetTimer(float a)
    {
        timer = a;
        slider.value = 0f;
    }

    public void SetGoingAway()
    {
        _isGoingAway = true;
    }

    public void SetTimeStop(bool a)
    {
        _timeStopped = a;
    }
}
