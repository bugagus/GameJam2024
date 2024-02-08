using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade {
    SS, S, A, B, C, D
}

public class ScoreManager : MonoBehaviour
{

    private const int goblinsToLevelUp = 10;

    // This thing is not used anymore
    private int _difficultyLevel;
    private int _playerScore;
    private int _maxCombo;
    private int _currentCombo;
    private int _goblinsServed;
    private int _goblinsFailed;
    private int _wordsFailed;

    public int GetScore() => _playerScore;

    //public void AddScore() => _playerScore += 50 * (int)Math.Pow(2, _difficultyLevel);

    public void AddScore() => _playerScore += (int) Math.Round((1 + (0.2 * _currentCombo)) * 50);

    public void SubtractScore() => _playerScore = Math.Max(0, _playerScore - 50);

    public void UpdateHighScore(Level level) => GameObject.FindObjectOfType<LevelManager>().UpdateHighScore(level, _playerScore);

    public int GetMaxCombo() => _maxCombo;

    public void ResetCombo() => _currentCombo = 0;

    public void AddWordsFailed() => _wordsFailed++;

    public void AddGoblinServed()
    {
        _goblinsServed++;
        _currentCombo++;
        _maxCombo = Math.Max(_maxCombo, _currentCombo);
        AddScore();

        if (_goblinsServed % goblinsToLevelUp == 0)
            _difficultyLevel++;
    
        Debug.Log("Goblins servidos: " + _goblinsServed);
    }

    public void AddGoblinFailed()
    {
        _goblinsFailed++;
        SubtractScore();
        
        Debug.Log("Goblins fallados: " + _goblinsFailed);
    }

    public int GetDifficulty() => _difficultyLevel;

    public void AddDifficulty() => _difficultyLevel++;

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
