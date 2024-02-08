using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] private TextWobble _bigTextColorScript;
    [SerializeField] private float smallTimer;
    [SerializeField] private float normalTimer;
    [SerializeField] private float bigTimer;
    public Transform spawnPos;
    private List<Goblin> goblinList = new();
    private EnemyGenerator enemyGenerator;
    [SerializeField] private AbilityManager _abilityManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
        _abilityManager = FindObjectOfType<AbilityManager>();
        StartGame();
    }

    public void StartGame()
    {
        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall(3.0f * i, () => { SpawnEnemy(); }, false);
        }
        GetComponent<CameraManager>().FindCamera();
    }

    public void SpawnEnemy()
    {
        enemyGenerator.SpawnEnemy();
    }

    public void AddGoblin(Goblin goblin)
    {
        int emptyIndex = FirstEmptyIndex();
        Debug.Log("Voy al index" + emptyIndex);
        goblinList.Add(goblin);
        goblin.Advance(positions[emptyIndex]);
    }

    public void RemoveGoblin(Goblin goblin)
    {
        int goblinIndex = goblinList.IndexOf(goblin);
        if (goblinIndex == -1) return;
        
        if (goblinIndex == 0){
            _bigTextColorScript.AllRed();
        }

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

    public void AutoServe()
    {
        List<Goblin> goblinListAux = new ();
        foreach(Goblin a in goblinList)
        {
            goblinListAux.Add(a);
        }
        goblinList.Clear();
        foreach(Goblin a in goblinListAux)
        {
            a.GoAway();
        }
        for (int i = 0; i < 5; i++)
        {
            DOVirtual.DelayedCall(3.0f * i, () => { SpawnEnemy(); }, false);
        }
    }

    public float GetTimerGoblin(EnemyType type)
    {
        switch(type){
            case EnemyType.NormalGoblin:
                return normalTimer;
            case EnemyType.BigGoblin:
                return bigTimer;
            case EnemyType.SmallGoblin:
                return smallTimer;
        }
        return 0f;
    }

    public List<Goblin> GetGoblinList() => goblinList;

}
