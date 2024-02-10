using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTiming;
    private CinemachineVirtualCamera _vc;
    private CinemachineBasicMultiChannelPerlin _vcPerlin;

    public void FindCamera()
    {
        _vc = FindObjectOfType<CinemachineVirtualCamera>();
        _vcPerlin = _vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        Debug.Log("Shake de camara");
        ProcessShake();
    }

    private void ProcessShake()
    {
        _vcPerlin.m_AmplitudeGain = shakeIntensity * Time.timeScale;
        DOVirtual.DelayedCall(shakeTiming, ()=> {_vcPerlin.m_AmplitudeGain = 0;});
    }
}
