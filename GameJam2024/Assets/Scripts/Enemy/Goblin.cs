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
        float difficultyLevel = _gameManager.GetComponent<ScoreManager>().GetDifficulty();
        float timer = (1 + (difficultyLevel * difficultyFactor)) * gameManager.GetTimerGoblin(enemyType);
        goblinTimer.SetTimer(timer);
    }

    private void OnEnable()
    {
        transform.position = gameManager.spawnPos.position;
        SetTimer();
        gameManager.AddGoblin(this);
    }
    
    private void Update()
    {
        if(_isAdvancing)
        {
            if(_transform.position.x <= _desiredPos.position.x)
            {
                Stop();
            }
        }else
        {
            if(_rb.velocity != Vector3.zero)
            {
                _rb.velocity = Vector3.zero;
            }
        }

    }
    
    #region MOVIMIENTO
    public void Advance(Transform pos)
    {
        _rb.velocity = new Vector3(-velocity, 0f, 0f);
        _isAdvancing = true;
        _desiredPos = pos;
    }

    public void Stop()
    {
        _isAdvancing = false;
        _transform.position = new Vector3(_desiredPos.position.x, _transform.position.y, _transform.position.z);
        _rb.velocity = Vector3.zero;
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
