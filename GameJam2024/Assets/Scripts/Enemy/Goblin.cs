using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] Transform[] positions = new Transform[7];
    [SerializeField] float velocity;
    public EnemyType enemyType;
    private GameSettings gameSettings;
    private GoblinTimer goblinTimer;
    private int _nPos;
    private bool _isAdvancing;
    private bool _isGoingAway;
    private Rigidbody _rb;
    private Transform _transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        gameSettings = FindObjectOfType<GameSettings>();
        goblinTimer = GetComponentInChildren<GoblinTimer>();
        _isGoingAway = false;
    }

    private void OnEnable()
    {
        _isGoingAway = false;
        _nPos = 0;
        transform.position = positions[_nPos].position;
        gameSettings.OnSpawn += Advance;
        switch (enemyType.ToString())
        {
            case "NormalGoblin":
                goblinTimer.SetTimer(gameSettings.GetNormalTime());
                break;

            case "SmallGoblin":
                goblinTimer.SetTimer(gameSettings.GetSmallTime());
                break;

            case "BigGoblin":
                goblinTimer.SetTimer(gameSettings.GetBigTime());
                break;
        }
    }
    
    private void Update()
    {
        if(_isAdvancing)
        {
            if(_transform.position.x >= positions[_nPos].position.x)
            {
                Stop();
            }
        }
    }
    
    #region MOVIMIENTO
    private void Advance(object sender, EventArgs e)
    {
        if(!_isGoingAway)
        {
            if(_nPos == 4)
            {
                FindObjectOfType<InputManager>().SetNextGoblin(GetComponent<MorseCode>());
            }else
            {
                _rb.velocity = new Vector3(velocity, 0f, 0f);
            }
            _isAdvancing = true;
        }
    }

    public void Stop()
    {
        _isAdvancing = false;
        _transform.position = new Vector3(positions[_nPos].position.x, _transform.position.y, _transform.position.z);
        _rb.velocity = Vector3.zero;
        _nPos++;
    }

    public void GoAway()
    {
        DOTweenModulePhysics.DOMoveZ(_rb, 2.0f, 2.0f, false);
        _isGoingAway = true;
        gameSettings.SpawnEnemy();
        DOVirtual.DelayedCall(2.0f, ()=> { DOTweenModulePhysics.DOMoveX(_rb, positions[6].position.x, 7.0f, false);});
    }

    public void HasBeenServed()
    {
        GoAway();
    }
    #endregion

}
