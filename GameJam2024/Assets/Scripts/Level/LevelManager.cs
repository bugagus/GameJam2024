using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Level _currentLevel;
    private Dictionary<Level, LevelType> _levelDefinitions;

    private Dictionary<Level, bool> _unlockedLevel = new()
    {
        {Level.Day1, true},
        {Level.Day2, false},
        {Level.Day3, false},
        {Level.InfiniteMode, false},
    };

    private Dictionary<Level, int> _levelHighScores = new()
    {
        {Level.Day1, 0},
        {Level.Day2, 0},
        {Level.Day3, 0},
        {Level.InfiniteMode, 0},
    };

    void Start()
    {
        GameObject.DontDestroyOnLoad(this);

        _levelDefinitions = LevelDefinition.DefineLevels();
    }

    void Update()
    {
        
    }
    public Level GetCurrentLevel => _currentLevel;
    public void SetCurrentLevel(Level level) => _currentLevel = level;

    public void UnlockNextLevel(Level level)
    {
        int numberOfLevels = Enum.GetNames(typeof(Level)).Length;
        int levelId = (int) level;

        if (levelId + 1 < numberOfLevels)
        {
            Level nextLevel = (Level) levelId + 1;
            _unlockedLevel[nextLevel] = true;  
            Debug.Log("He desbloqueado el nivel " + nextLevel); 
        }
    } 

    public Dictionary<Level, LevelType> GetLevelDefinitions => _levelDefinitions;
    
    public void UpdateHighScore(Level level, int score) => _levelHighScores[level] = Math.Max(_levelHighScores[level], score); 
}
