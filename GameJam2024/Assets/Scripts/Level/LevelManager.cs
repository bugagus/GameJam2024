using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Level _currentLevel;
    private Dictionary<Level, LevelType> _levelDefinitions;
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
}
