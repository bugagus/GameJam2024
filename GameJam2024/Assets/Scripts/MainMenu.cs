using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private LevelManager _levelManager;
    public void gameScene()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        SceneManager.LoadScene("LevelSelector");

        //_levelManager = FindObjectOfType<LevelManager>();
        //_levelManager.SetCurrentLevel(Level.Day1);
    }

    public void closeGame()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }
}
