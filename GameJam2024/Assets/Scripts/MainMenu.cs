using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void gameScene()
    {
        //SceneManager.LoadScene("Game");
        SceneManager.LoadScene("Main");
    }

    public void closeGame()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }
}
