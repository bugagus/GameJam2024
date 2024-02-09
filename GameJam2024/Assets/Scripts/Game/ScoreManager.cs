using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Grade {
    SS, S, A, B, C, D
}

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Image stars;
    [SerializeField] private float amountStars;
    [SerializeField] private float constantStars;
    private GlobalCanvas _globalCanvas;
    private int _playerScore;
    private int _maxCombo;
    private int _currentCombo;
    private int _goblinsServed;
    private int _goblinsFailed;
    private int _wordsFailed;

    void Awake()
    {
        _globalCanvas = FindObjectOfType<GlobalCanvas>();
    }
    void Update()
    {
        stars.fillAmount -= constantStars*Time.deltaTime;
    }

    public int GetScore() => _playerScore;

    public void AddScore()=> _playerScore += (int) Math.Round((1 + (0.2 * _currentCombo)) * 50);

    public void SubtractScore() => _playerScore = Math.Max(0, _playerScore - 50);

    public void UpdateHighScore(Level level) => GameObject.FindObjectOfType<LevelManager>().UpdateHighScore(level, _playerScore);

    public int GetMaxCombo() => _maxCombo;

    public void ResetCombo() => _currentCombo = 0;

    public void AddWordsFailed() => _wordsFailed++;

    public int GetGoblinsServed() => _goblinsServed;

    public float GetFillAmount ()=> stars.fillAmount;


    public void AddGoblinServed()
    {
        stars.fillAmount += amountStars;
        _goblinsServed++;
        _currentCombo++;
        _maxCombo = Math.Max(_maxCombo, _currentCombo);
        AddScore();
        _globalCanvas.SetPoints(GetScore());
        _globalCanvas.SetCombo(1 + (0.2 * _currentCombo));
    }

    public void AddGoblinFailed()
    {
        stars.fillAmount -= 0.75f*amountStars;
        _goblinsFailed++;
        SubtractScore();
        _globalCanvas.SetPoints(GetScore());
        _globalCanvas.SetCombo(1 + (0.2 * _currentCombo));
    }


    public Grade GetGrade()
    {
        int totalGoblins = _goblinsServed + _goblinsFailed;
        float accuracy = _goblinsServed / totalGoblins;

        if (_wordsFailed == 0 && _goblinsFailed == 0)
            return Grade.SS;
        else if (_goblinsFailed == 0)
            return Grade.S;
        else if (accuracy >= 0.85)
            return Grade.A;
        else if (accuracy >= 0.70)
            return Grade.B;
        else if (accuracy >= 0.50)
            return Grade.C;
        else
            return Grade.D;
    }
}
