using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
public class GameSettings : MonoBehaviour
{
    [SerializeField] private float NormalGoblinInitialWaitTime;
    [SerializeField] private float SmallGoblinInitialWaitTime;
    [SerializeField] private float BigGoblinInitialWaitTime;
    [SerializeField] Transform[] positions;

    [SerializeField] private TextWobble _bigTextColorScript;


    public Transform spawnPos;
    private List<Goblin> goblinList = new();
    private EnemyGenerator enemyGenerator;
    private int goblinsServedLevel;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
        StartGame();
    }

    public void AddGoblinServed()
    {
        goblinsServedLevel++;
        if (goblinsServedLevel % 11 == 10)
        {
            goblinsServedLevel = 0;
            AddDificulty();
        }
    }

    public void AddDificulty()
    {
        NormalGoblinInitialWaitTime -= NormalGoblinInitialWaitTime / 4;
        SmallGoblinInitialWaitTime -= SmallGoblinInitialWaitTime / 4;
        BigGoblinInitialWaitTime -= BigGoblinInitialWaitTime / 4;
    }

    public void StartGame()
    {
        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall(3.0f * i, () => { SpawnEnemy(); }, false);
        }
    }

    public void SpawnEnemy()
    {
        enemyGenerator.SpawnEnemy();
    }

    public float GetNormalTime()
    {
        return NormalGoblinInitialWaitTime;
    }

    public float GetSmallTime()
    {
        return SmallGoblinInitialWaitTime;
    }

    public float GetBigTime()
    {
        return BigGoblinInitialWaitTime;
    }

    public void AddGoblin(Goblin goblin)
    {
        int emptyIndex = FirstEmptyIndex();
        goblinList.Add(goblin);
        goblin.Advance(positions[emptyIndex]);
    }

    public void RemoveGoblin(Goblin goblin)
    {
        Debug.Log("Quito goblin");


        int goblinIndex = goblinList.IndexOf(goblin);
        if (goblinIndex == -1) return;
        
        if (goblinIndex == 0) _bigTextColorScript.AllRed();

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
        SpawnEnemy();

    }

    private int FirstEmptyIndex()
    {
        return goblinList.Count;
    }


}
