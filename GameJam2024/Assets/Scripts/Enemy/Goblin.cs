using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private GameObject _gameManager;
    [SerializeField] private Transform exitPosition;
    [SerializeField] float velocity;
    public EnemyType enemyType;
    private GameManager gameManager;
    private GoblinTimer goblinTimer;
    private Transform _desiredPos;
    private bool _isAdvancing;
    private Rigidbody _rb;
    private Transform _transform;

    //private readonly Dictionary<EnemyType, float> _goblinTimers = new() {
    //    {EnemyType.NormalGoblin, 30f},
    //    {EnemyType.SmallGoblin,  20f},
    //    {EnemyType.BigGoblin,    40f}
    //};
    private const float difficultyFactor = 0.2f;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        goblinTimer = GetComponentInChildren<GoblinTimer>();
    }

    private void SetTimer()
    {
        float timer = gameManager.GetTimerGoblin(enemyType);
        goblinTimer.SetTimer(timer);
    }

    private void OnEnable()
    {
        _isAdvancing = false;
        transform.position = gameManager.spawnPos.position;
        SetTimer();
        //gameManager.AddGoblin(this);
    }
    
    private void Update()
    {
        if(_isAdvancing)
        {
            _rb.velocity = new Vector3(-velocity, 0f, 0f);
            if(_transform.position.x <= _desiredPos.position.x)
            {
                Stop();
            }
        }
        if(!_isAdvancing && _rb.isKinematic == false)
        {
            _rb.isKinematic = true;
        }

    }
    
    #region MOVIMIENTO
    public void Advance(Transform pos)
    {
        _rb.isKinematic = false;
        _isAdvancing = true;
        _desiredPos = pos;
    }

    public void Stop()
    {
        _rb.isKinematic = true;
        _isAdvancing = false;
        _transform.position = new Vector3(_desiredPos.position.x, _transform.position.y, _transform.position.z);
    }

    public void GoAway()
    {
        GetComponentInChildren<Animator>().SetTrigger("DisappearText");
        gameManager.RemoveGoblin(this);
        goblinTimer.SetGoingAway();
        DOTweenModulePhysics.DOMoveZ(_rb, 2.0f, 2.0f, false);
        DOVirtual.DelayedCall(2.0f, ()=> { DOTweenModulePhysics.DOMoveX(_rb, exitPosition.position.x, 7.0f, false);});
    }

    public void HasBeenServed()
    {
        FindObjectOfType<AbilityManager>().AddGoblin();
        _gameManager.GetComponent<ScoreManager>().AddGoblinServed();
        GoAway();
    }
    #endregion

}
