using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private LevelManager _levelManager;
    public void LoadDay1()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        SceneManager.LoadScene("Day1");

        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.SetCurrentLevel(Level.Day1);
    }

        public void LoadDay2()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        SceneManager.LoadScene("Day2");

        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.SetCurrentLevel(Level.Day2);
    }

        public void LoadDay3()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        SceneManager.LoadScene("Day3");

        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.SetCurrentLevel(Level.Day3);
    }
}
