using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] Transform[] positions = new Transform[5];
    [SerializeField] float velocity;
    [SerializeField] protected TextMeshProUGUI textMorse;
    private Transform desiredPosition;
    private int _nPos;
    protected MorseCodeGenerator _morseGenerator;
    private bool _isAdvancing;
    private Rigidbody _rb;
    private Transform _transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _morseGenerator = FindObjectOfType<MorseCodeGenerator>();
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
        _rb.velocity = new Vector3(velocity, 0f, 0f);
        _isAdvancing = true;
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

    }

}
