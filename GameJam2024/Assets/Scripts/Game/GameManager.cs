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
    private bool _gameFinished;
    private bool _timerOn;
    public Transform spawnPos;
    private List<Goblin> goblinList = new();
    private ScoreManager _scoreManager;
    private EnemyGenerator _enemyGenerator;
    private LevelManager _levelManager;
    private GlobalCanvas _globalCanvas;
    private SoundManager _soundManager;
    private LevelType _level;
    [SerializeField] private AbilityManager _abilityManager;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MusicPlayer>().PlayMusicGame();
        _gameFinished = false;
        //GameObject.DontDestroyOnLoad(this);
        _globalCanvas = FindObjectOfType<GlobalCanvas>();
        _scoreManager = GetComponent<ScoreManager>();
        _enemyGenerator = FindObjectOfType<EnemyGenerator>();
        _levelManager = FindObjectOfType<LevelManager>();
        _soundManager = FindObjectOfType<SoundManager>();

        _level = _levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel];

        Debug.Log(_levelManager.GetLevelDefinitions[_levelManager.GetCurrentLevel].timer);
        Debug.Log(_level.timer);
        _abilityManager = FindObjectOfType<AbilityManager>();
        StartGame();
    }

    void Update()
    {
        if (_timerOn && !_gameFinished)
        {
            _targetTime -= Time.deltaTime;
            _globalCanvas.SetTimer(_targetTime);

            if (_targetTime <= 0.0f)
                FinishGame();
        }
        if (_scoreManager.GetFillAmount() <= 0f)
        {
            GameOver();
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


    private void FinishGame()
    {
        Time.timeScale = 0f;
        _gameFinished = true;
        _soundManager.PlayAudioClip(Sound.levelCompleted);
        UpdateHighScore();
        _levelManager.UnlockNextLevel(_levelManager.GetCurrentLevel);

        Debug.Log("Score: " + _scoreManager.GetScore());
        Debug.Log("Max Combo: " + _scoreManager.GetMaxCombo());
        Debug.Log("Grade: " + _scoreManager.GetGrade());

        _globalCanvas.ShowResultsScreen(_scoreManager.GetScore(), _scoreManager.GetMaxCombo(), _scoreManager.GetGrade());
        _levelManager.GetLevelGrades[_levelManager.GetCurrentLevel] = _scoreManager.GetGrade();
    }

    private void GameOver()
    {
        _soundManager.PlayAudioClip(Sound.gameOver);
        Debug.Log("GAME OVER");
        _globalCanvas.ShowGameOver(_scoreManager.GetScore(), _scoreManager.GetMaxCombo(), _scoreManager.GetGrade());
        _levelManager.GetLevelGrades[_levelManager.GetCurrentLevel] = _scoreManager.GetGrade();
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

        if (goblinIndex == 0)
        {
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
        List<Goblin> goblinListAux = new();
        foreach (Goblin a in goblinList)
        {
            goblinListAux.Add(a);
        }
        goblinList.Clear();
        foreach (Goblin a in goblinListAux)
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
        switch (type)
        {
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
        PlaySound(Sound.failedWord);
    }

    private void UpdateHighScore() => _scoreManager.UpdateHighScore(_level.level);

    public List<Goblin> GetGoblinList() => goblinList;

    public void PlaySound(Sound sound) => _soundManager.PlayAudioClip(sound);

}
