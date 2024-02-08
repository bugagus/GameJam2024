using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    Day1,
    Day2,
    Day3,
    InfiniteMode
}

public struct LevelType
{
    public Level level;
    public float timer;
    public bool infinite;
    public List<EnemyType> enemies;

    public LevelType(Level level, float timer, bool infinite, List<EnemyType> enemies) {
        this.level = level;
        this.timer = timer;
        this.infinite = infinite;
        this.enemies = enemies;
    }
}

public static class LevelDefinition
{

    public static Dictionary<Level, LevelType> DefineLevels() {

        LevelType Day1 = new (
            Level.Day1,
                15f,
                false,
                new List<EnemyType>
                {
                    EnemyType.NormalGoblin
                }
        );

        LevelType Day2 = new (
            Level.Day2,
                60f,
                false,
                new List<EnemyType>
                {
                    EnemyType.NormalGoblin,
                    EnemyType.SmallGoblin
                }
        );

        LevelType Day3 = new (
            Level.Day3,
                60f,
                false,
                new List<EnemyType>
                {
                    EnemyType.NormalGoblin,
                    EnemyType.SmallGoblin,
                    EnemyType.BigGoblin
                }
        );

        LevelType InfiniteMode = new (
            Level.InfiniteMode,
                0f,
                true,
                new List<EnemyType>
                {
                    EnemyType.NormalGoblin,
                    EnemyType.SmallGoblin,
                    EnemyType.BigGoblin
                }
        );

        Dictionary<Level, LevelType> levelDefinitions = new()
        {
            { Level.Day1, Day1 },
            { Level.Day2, Day2 },
            { Level.Day3, Day3 },
            { Level.InfiniteMode, InfiniteMode }
        };

        return levelDefinitions;
    }

}
