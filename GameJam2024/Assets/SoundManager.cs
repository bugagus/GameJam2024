using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Sound
{
    correctWord, failedWord, buttonClick, levelCompleted, gameOver, stopTime, serveAll, input
}

public class SoundManager : MonoBehaviour
{

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _correctWord;
    [SerializeField] private AudioClip _failedWord;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _levelCompleted;
    [SerializeField] private AudioClip _gameOver;
    [SerializeField] private AudioClip _stopTime;
    [SerializeField] private AudioClip _serveAll;
    [SerializeField] private AudioClip _input;

    private Dictionary<Sound, AudioClip> _soundList = new();

    void Start()
    {
        GameObject.DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
        InitializeSoundList();
        //PlayAudioClip(Sound.failedWord);
    }
    private void InitializeSoundList()
    {
        _soundList.Add(Sound.correctWord, _correctWord);
        _soundList.Add(Sound.failedWord, _failedWord);
        _soundList.Add(Sound.buttonClick, _buttonClick);
        _soundList.Add(Sound.levelCompleted, _levelCompleted);
        _soundList.Add(Sound.gameOver, _gameOver);
        _soundList.Add(Sound.stopTime, _stopTime);
        _soundList.Add(Sound.serveAll, _serveAll);
        _soundList.Add(Sound.input, _input);
    }

    public void PlayAudioClip(Sound sound)
    {
        if(Time.timeScale==0) return;
        _audioSource.pitch = 1;
        float random = Random.Range(0f, 0.03f);
        _audioSource.pitch += random;
        Debug.Log(_soundList[sound]);
        _audioSource.clip = _soundList[sound];
        _audioSource.Play();
    }

}
