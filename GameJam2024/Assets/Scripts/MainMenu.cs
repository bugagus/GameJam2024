using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private LevelManager _levelManager;
    private SoundManager _soundManager;

    public void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }

    public void gameScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayButtonSound()
    {
        _soundManager.PlayAudioClip(Sound.buttonClick);
    }

    public void closeGame()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }
}
