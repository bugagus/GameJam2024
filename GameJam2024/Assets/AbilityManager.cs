using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private int _abilitiesUnlocked;
    [SerializeField] int ToFreezeGoblins, ToServeGoblins;
    [SerializeField] private float freezeTime;
    [SerializeField] private TMP_Text toFreezeText,toServeText;
    private int goblinToFreeze, goblinToServe; 
    List<Action> abilities = new();
    public static bool timeStop;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    private int _goblinToPower;

    private void Start()
    {
        abilities.Add(TimeStopAbility);
        abilities.Add(AutoServeAbility);
    }
    public void StartGame()
    {
        _gameManager = GetComponent<GameManager>();
        _scoreManager = GetComponent<ScoreManager>();
    }
    public void UsePower(int i)
    {
        if(i < _abilitiesUnlocked) abilities[i].Invoke();
    }
    public void UnlockPower()
    {
        _abilitiesUnlocked++;
    }

    private void TimeStopAbility()
    {
        _gameManager.PlaySound(Sound.stopTime);
        GoblinTimer[] _goblinsTimers = FindObjectsOfType<GoblinTimer>();
        foreach(GoblinTimer _gt in _goblinsTimers)
        {
            _gt.SetTimeStop(true);
            DOVirtual.DelayedCall(freezeTime, ()=>{_gt.SetTimeStop(false);}, false);
        }
        FindObjectOfType<ClockAnimation>().StartAnimation();

    }

    private void AutoServeAbility()
    {
        List<Goblin> goblinListAux = FindObjectOfType<GameManager>().GetGoblinList();
        int points = goblinListAux.Count;
        _gameManager.PlaySound(Sound.serveAll);
        _gameManager.AutoServe();
        AddGoblin();
        for(int i = 0; i < 5 ; i++) _scoreManager.AddGoblinServed();


    }

    public void CheckAbility()
    {
        if(_goblinToPower % ToFreezeGoblins == 0)
            UsePower(0);
        if(_goblinToPower % ToServeGoblins == 0)
            UsePower(1);
    }

    public void AddGoblin()
    {
        _goblinToPower++;

        goblinToFreeze = ToFreezeGoblins-_goblinToPower%ToFreezeGoblins;
        goblinToServe = ToServeGoblins-_goblinToPower%ToServeGoblins;

        toFreezeText.text = goblinToFreeze.ToString();
        toServeText.text = goblinToServe.ToString();
    }

    public int GetUnlockedPowers ()=> _abilitiesUnlocked;
}
