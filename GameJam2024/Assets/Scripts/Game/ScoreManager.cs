using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private const int goblinsToLevelUp = 10;

    private int _difficultyLevel;
    private int _playerScore;
    private int _goblinsServed;
    private int _goblinsFailed;

    public void AddScore()
    {
        _playerScore += 50 * (int)Math.Pow(2, _difficultyLevel);
        Debug.Log(_playerScore);
    }

    public void SubtractScore() => _playerScore = Math.Max(0, _playerScore - 50);

    public void UpdateHighScore(Level level) => GameObject.FindObjectOfType<LevelManager>().UpdateHighScore(level, _playerScore);

    public void AddGoblinServed()
    {
        _goblinsServed++;
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
}
