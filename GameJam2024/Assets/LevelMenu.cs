using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private LevelManager _levelManager;

    private RectTransform _diasTransform;
    private RectTransform _previousButtonTransform;
    private RectTransform _nextButtonTransform;

    private int _currentPos;

    private float[] _levelPositions = { 0.0f, -1500.0f, -3000.0f, -4500.0f };

    private void Awake()
    {
        _currentPos = 0;
        _diasTransform = GameObject.Find("Dias").GetComponent<RectTransform>();
        _previousButtonTransform = GameObject.Find("PreviousLevelButton").GetComponent<RectTransform>();
        _nextButtonTransform = GameObject.Find("NextLevelButton").GetComponent<RectTransform>();
    }

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

    public void ShowNextLevel()
    {
        _currentPos++;

        if (_currentPos >= 0 && _currentPos <= 3)
        {
            _diasTransform.DOLocalMoveX(_levelPositions[_currentPos], 0.5f, true);
            _diasTransform.DOLocalRotate(new Vector3(720, 0, 0), 0.75f, RotateMode.FastBeyond360);
        }
        else
        {
            _currentPos--;
            RectTransform aux = _nextButtonTransform;
            _nextButtonTransform.DOShakePosition(0.5f, 10);
        }
    }

    public void ShowPreviousLevel()
    {
        _currentPos--;
        if (_currentPos >= 0 && _currentPos <= 3)
        {
            _diasTransform.DOLocalMoveX(_levelPositions[_currentPos], 0.5f, true);
            _diasTransform.DOLocalRotate(new Vector3(720, 0, 0), 0.75f, RotateMode.FastBeyond360);
        }
        else
        {
            _currentPos++;
            _previousButtonTransform.DOShakePosition(0.5f, 10);
        }
    }
}
