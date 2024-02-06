using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int _difficultyLevel;
    private int _playerScore;
    private int _goblinsServed;
    private int _goblinsFailed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddScore()
    {
        _playerScore += 50 * (int)Math.Pow(2, _difficultyLevel);
        Debug.Log(_playerScore);
    }

    public void SubtractScore() => _playerScore = Math.Max(0, _playerScore - 50);
    public void AddGoblinServed()
    {
        _goblinsServed++;
        Debug.Log("Goblins servidos: " + _goblinsServed);
    }

    public void AddGoblinFailed()
    {
        _goblinsFailed++;
        Debug.Log("Goblins fallados: " + _goblinsFailed);
    }

    // TODO If we feel like it maybe eventually change all getters to this one-line statements.
    // Nvm, read about {get; set;} later!!!!!
    public int GetDifficulty() => _difficultyLevel;

    public void AddDifficulty() => _difficultyLevel++;
}
