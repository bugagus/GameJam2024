using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] Transform[] positions = new Transform[6];
    [SerializeField] float velocity;
    [SerializeField] private int _numberOfLetters;
    private Transform desiredPosition;
    private int _nPos;
    private bool _isAdvancing;
    private Rigidbody _rb;
    private Transform _transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        GoAway();
        Advance();
    }

    protected void OnEnable()
    {
        _nPos = 0;
    }
    
    protected void Update()
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
    public void Advance()
    {
        if(_nPos == 5)
        {
            GoAway();
        }else
        {
            _rb.velocity = new Vector3(velocity, 0f, 0f);
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
    #endregion

    protected void GoAway()
    {
        DOTweenModulePhysics.DOMoveZ(_rb, 2.0f, 2.0f, false);
        DOVirtual.DelayedCall(2.0f, ()=> { DOTweenModulePhysics.DOMoveX(_rb, positions[5].position.x, 7.0f, false);});
    }

    protected void InitTimer(float i)
    {
        DOVirtual.DelayedCall(i, ()=> { GoAway();});
    }

    public int GetNumberOfLetters()
    {
        return _numberOfLetters;
    }

}
