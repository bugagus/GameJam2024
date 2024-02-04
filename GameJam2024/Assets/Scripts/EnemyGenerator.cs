using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    private const int EnemiesOnScreen = 5;
    private const int TypesOfEnemies = 3;

    [SerializeField] private GameObject _enemyPool;
    GameObject[] _enemyList = new GameObject[EnemiesOnScreen * TypesOfEnemies];
    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hiii");

        InitializeEnemyList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeEnemyList() {
        Transform transform = _enemyPool.transform;
        int i = 0;
        foreach (Transform item in transform) {
            _enemyList[i] = item.gameObject;
            Debug.Log(_enemyList[i].name);
            i++;
        }
    }
}
