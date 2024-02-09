using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    private const int EnemiesOnScreen = 10;
    private Level _currentLevel;

    [SerializeField] private GameObject _enemyPool;

    private Dictionary<EnemyType, GameObject[]> _enemyList = new() {
        {EnemyType.NormalGoblin, new GameObject[EnemiesOnScreen]},
        {EnemyType.SmallGoblin,  new GameObject[EnemiesOnScreen]},
        {EnemyType.BigGoblin,    new GameObject[EnemiesOnScreen]}
    };

    // Spawn probability of each enemy type.
    // Sum of values should be 1.
    private Dictionary<EnemyType, float> _enemyRareness = new() {
        {EnemyType.NormalGoblin, 0.5f}, {EnemyType.SmallGoblin, 0.3f}, {EnemyType.BigGoblin, 0.2f}
    };
    


    // Start is called before the first frame update
    void Start()
    {
        _currentLevel = FindObjectOfType<LevelManager>().GetCurrentLevel;
        InitializeEnemyList();
    }

    // Update is called once per fram

    private void InitializeEnemyList() {

        Dictionary<EnemyType, int> _enemyCount = new() {
            {EnemyType.NormalGoblin, 0}, {EnemyType.SmallGoblin, 0}, {EnemyType.BigGoblin, 0}
        };

        Transform transform = _enemyPool.transform;
        foreach (Transform item in transform) {
            EnemyType enemyType = item.gameObject.GetComponent<Goblin>().enemyType;

            _enemyList[enemyType][_enemyCount[enemyType]] = item.gameObject;
            _enemyCount[enemyType]++;
        }
    }

    private EnemyType _GetRandomEnemyType() {
        float random = Random.Range(0f, 1f);;
        switch(_currentLevel)
        {
            case Level.Day1:
                random = Random.Range(0.5f, 1f);
            break;
            case Level.Day2:
                random = Random.Range(0.3f, 1f);
            break;
            case Level.Day3:
                random = Random.Range(0f, 1f);
            break;

        }
        Debug.Log(random);
        foreach (KeyValuePair<EnemyType, float> entry in _enemyRareness) {
            Debug.Log(entry.Value);
            Debug.Log(entry.Key);
            if (random > entry.Value) {
                return entry.Key;
            }
        }

        return EnemyType.NormalGoblin;  // Runaway return, should never execute.
    }

    private int _GetFirstDisabledEnemyObjectIndex(EnemyType enemyType) {
        GameObject[] enemyTypeList = _enemyList[enemyType];
        for (int i = 0; i < enemyTypeList.Length; i++) {
            GameObject enemy = enemyTypeList[i];
            if (!enemy.activeSelf) {
                return i;
            }
        }

        // TODO Should return an exception or some kind of error
        return 1;
    }

    // TODO Change all 'Enemy' to 'Goblin'
    public Goblin SpawnEnemy() {
        EnemyType enemyType = _GetRandomEnemyType();
        Debug.Log(enemyType);
        int firstFreeIndex = _GetFirstDisabledEnemyObjectIndex(enemyType);
        GameObject enemy = _enemyList[enemyType][firstFreeIndex];
        Debug.Log(enemy.name);
        enemy.SetActive(true);
        return enemy.GetComponent<Goblin>();
    }
}
