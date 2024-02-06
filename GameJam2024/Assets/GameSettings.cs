using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
public class GameSettings : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    public Transform spawnPos;
    private List<Goblin> goblinList = new();
    private EnemyGenerator enemyGenerator;
    private int _difficultyLevel;
    private int _playerScore;
    private int _goblinsServed;
    private int _goblinsFailed;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
        StartGame();
    }

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

    public void StartGame()
    {
        for(int i = 0; i < 5; i ++)
        {
            DOVirtual.DelayedCall(3.0f*i, ()=> {SpawnEnemy();} , false);
        }
    }

    public void SpawnEnemy()
    {
        enemyGenerator.SpawnEnemy();
    }

    public void AddGoblin(Goblin goblin)
    {
        int emptyIndex = FirstEmptyIndex();
        goblinList.Add(goblin);
        goblin.Advance(positions[emptyIndex]);
        if(emptyIndex == 0)
        {
            FindObjectOfType<InputManager>().SetNextGoblin(goblin.GetComponent<MorseCode>());
        }
    }

    public void RemoveGoblin(Goblin goblin)
    {
        Debug.Log("Quito goblin");
        int goblinIndex = goblinList.IndexOf(goblin);
        for(int i = goblinIndex; i < goblinList.Count-1; i++)
        {
            goblinList[i] = goblinList[i+1];
            goblinList[i].Advance(positions[i]);
            if(i == 0)
            {
                FindObjectOfType<InputManager>().SetNextGoblin(goblinList[i].GetComponent<MorseCode>());
            }
        }
        goblinList.RemoveAt(goblinList.Count - 1);
        SpawnEnemy();
    }

    private int FirstEmptyIndex()
    {
        return goblinList.Count;
    }


}
