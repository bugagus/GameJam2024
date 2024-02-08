using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private LevelType _level;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.DontDestroyOnLoad(this);
        _scoreManager = GetComponent<ScoreManager>();
        _enemyGenerator = FindObjectOfType<EnemyGenerator>();
        _levelManager = FindObjectOfType<LevelManager>();

        _level = _levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel];

        Debug.Log(_levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel].timer);
        Debug.Log(_level.timer);
        StartGame();
    }

    void Update() {
        if (_timerOn)
        {
            _targetTime -= Time.deltaTime;

            if (_targetTime <= 0.0f)
                EndTimer();
        }
        else
        {
            if (_showResults && goblinList.Count == 0)
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
    }

    private void StartTimer()
    {
        _targetTime = _level.timer;
        _timerOn = true;
    }

    private void EndTimer()
    {
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

        // Should go to level select screen
        _levelManager.SetCurrentLevel(Level.Day2);
        SceneManager.LoadScene("Day2");
    }

    public void SpawnGoblin()
    {
        Goblin goblin = _enemyGenerator.SpawnEnemy();
        int emptyIndex = FirstEmptyIndex();
        goblinList.Add(goblin);
        goblin.Advance(positions[emptyIndex]);
    } 

    public void RemoveGoblin(Goblin goblin)
    {
        Debug.Log("Quito goblin");


        int goblinIndex = goblinList.IndexOf(goblin);
        if (goblinIndex == -1) return;
        
        if (goblinIndex == 0) _bigTextColorScript.AllRed();

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
        if (_timerOn)
            SpawnGoblin();

    }

    private int FirstEmptyIndex()
    {
        return goblinList.Count;
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
    
}
