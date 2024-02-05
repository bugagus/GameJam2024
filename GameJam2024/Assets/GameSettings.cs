using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
class PositionData
{
    public Transform position;
    public bool occupied;
    
}
public class GameSettings : MonoBehaviour
{
    [SerializeField] private float NormalGoblinInitialWaitTime;
    [SerializeField] private float SmallGoblinInitialWaitTime;
    [SerializeField] private float BigGoblinInitialWaitTime;
    [SerializeField] Transform[] positions;
    private PositionData[] positionsManager;
    private EnemyGenerator enemyGenerator;
    public event EventHandler OnSpawn;
    private int goblinsServedLevel;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
        StartGame();
        for(int i = 0; i < positions.Length; i++)
        {
            positionsManager[i].position = positions[i];
            positionsManager[i].occupied = false;
        }
    }

    public void AddGoblinServed()
    {
        goblinsServedLevel++;
        if(goblinsServedLevel % 11 == 10)
        {
            goblinsServedLevel = 0;
            AddDificulty();
        }
    }

    public void AddDificulty()
    {
        NormalGoblinInitialWaitTime -= NormalGoblinInitialWaitTime/4;
        SmallGoblinInitialWaitTime -= SmallGoblinInitialWaitTime/4;
        BigGoblinInitialWaitTime -= BigGoblinInitialWaitTime/4;
    }

    public void StartGame()
    {
        Debug.Log(GetNextEmpty());
        for(int i = 0; i < 5; i ++)
        {
            DOVirtual.DelayedCall(3.0f*i, ()=> {SpawnEnemy();} , false);
        }
    }

    public void SpawnEnemy()
    {
        enemyGenerator.SpawnEnemy();
        OnSpawn?.Invoke(this, EventArgs.Empty);
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

    public Transform GetNextEmpty()
    {
        for(int i = 0; i < positionsManager.Length; i++)
        {
            if(!positionsManager[i].occupied)
            {
                return positionsManager[i].position;
            }
        }
        return null;

    }


}
