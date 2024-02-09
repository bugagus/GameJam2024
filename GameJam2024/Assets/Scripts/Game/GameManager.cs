using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] private TextWobble _bigTextColorScript;
    [SerializeField] private float smallTimer;
    [SerializeField] private float normalTimer;
    [SerializeField] private float bigTimer;
    private float _targetTime;
    private bool _timerOn;
    private bool _showResults;
    public Transform spawnPos;
    private List<Goblin> goblinList = new();
    private ScoreManager _scoreManager;
    private EnemyGenerator _enemyGenerator;
    private LevelManager _levelManager;
    private GlobalCanvas _globalCanvas;
    private LevelType _level;
    [SerializeField] private AbilityManager _abilityManager;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.DontDestroyOnLoad(this);
        _globalCanvas = FindObjectOfType<GlobalCanvas>();
        _scoreManager = GetComponent<ScoreManager>();
        _enemyGenerator = FindObjectOfType<EnemyGenerator>();
        _levelManager = FindObjectOfType<LevelManager>();

        _level = _levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel];

        Debug.Log(_levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel].timer);
        Debug.Log(_level.timer);
        _abilityManager = FindObjectOfType<AbilityManager>();
        StartGame();
    }

    void Update() {
        if (_timerOn)
        {
            _targetTime -= Time.deltaTime;
            _globalCanvas.SetTimer(_targetTime);

            if (_targetTime <= 0.0f)
                EndTimer();
        }
        else
        {
            if (_showResults || _scoreManager.GetFillAmount() == 0f)
                FinishGame();

        }
    }

    public void StartGame()
    {
        if (!_level.infinite)
            StartTimer();

        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall(3.0f * i, () => { SpawnGoblin(); }, false);
        }
        GetComponent<CameraManager>().FindCamera();
        FindObjectOfType<AbilityManager>().StartGame();
    }

    private void StartTimer()
    {
        _targetTime = _level.timer;
        _timerOn = true;
    }

    private void EndTimer()
    {
        Debug.Log("SE ACABO EL TIMER");
        _timerOn = false;
        _showResults = true;
    }

    private void FinishGame()
    {
        _showResults = false;

        // TODO Show results screen

        UpdateHighScore();

        Debug.Log("Score: " + _scoreManager.GetScore());
        Debug.Log("Max Combo: " + _scoreManager.GetMaxCombo());
        Debug.Log("Grade: " + _scoreManager.GetGrade());

        _globalCanvas.FinishGame();

        // Should go to level select screen
        //SceneManager.LoadScene("LevelSelector");
    }

    public void SpawnGoblin()
    {
        Goblin goblin = _enemyGenerator.SpawnEnemy();
        int emptyIndex = FirstEmptyIndex();
        Debug.Log("Voy al index" + emptyIndex);
        goblinList.Add(goblin);
        goblin.Advance(positions[emptyIndex]);
    } 

    public void RemoveGoblin(Goblin goblin)
    {
        int goblinIndex = goblinList.IndexOf(goblin);
        if (goblinIndex == -1) return;
        
        if (goblinIndex == 0){
            _bigTextColorScript.AllRed();
        }

        for (int i = goblinIndex; i < goblinList.Count - 1; i++)
        {
            Debug.Log(i + " Contador de cuantos hay" + goblinList.Count);
            goblinList[i] = goblinList[i + 1];
            if (goblinList[i] != null)
            {
                goblinList[i].Advance(positions[i]);
            }
        }
        goblinList.RemoveAt(goblinList.Count - 1);
        //if (_timerOn)
            SpawnGoblin();

    }

    private int FirstEmptyIndex()
    {
        return goblinList.Count;
    }

    public void AutoServe()
    {
        List<Goblin> goblinListAux = new ();
        foreach(Goblin a in goblinList)
        {
            goblinListAux.Add(a);
        }
        goblinList.Clear();
        foreach(Goblin a in goblinListAux)
        {
            a.GoAway();
        }
        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall(3.0f * i, () => { SpawnGoblin(); }, false);
        }
    }

    public float GetTimerGoblin(EnemyType type)
    {
        switch(type){
            case EnemyType.NormalGoblin:
                return normalTimer;
            case EnemyType.BigGoblin:
                return bigTimer;
            case EnemyType.SmallGoblin:
                return smallTimer;
        }
        return 0f;
    }

    public void ResetCombo()
    {
        _scoreManager.ResetCombo();
        _scoreManager.AddWordsFailed();
    }

    private void UpdateHighScore() => _scoreManager.UpdateHighScore(_level.level);

    public List<Goblin> GetGoblinList() => goblinList;

}
