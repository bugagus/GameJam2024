using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    private LevelManager _levelManager;
    private SoundManager _soundManager;
    private RectTransform _diasTransform;
    private RectTransform _previousButtonTransform;
    private RectTransform _nextButtonTransform;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Image[] levelButtons;

    private int _currentPos;

    private float[] _levelPositions = { 0.0f, -1500.0f, -3000.0f, -4500.0f };

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _currentPos = 0;
        _levelManager = FindObjectOfType<LevelManager>();
        _diasTransform = GameObject.Find("Dias").GetComponent<RectTransform>();
        _previousButtonTransform = GameObject.Find("PreviousLevelButton").GetComponent<RectTransform>();
        _nextButtonTransform = GameObject.Find("NextLevelButton").GetComponent<RectTransform>();

        if (!_levelManager.GetUnlockedLevels[Level.Day2])
        {
            var tempColor = levelButtons[1].color;
            tempColor.a = 0.5f;
            levelButtons[1].color = tempColor;
        }

        if (!_levelManager.GetUnlockedLevels[Level.Day3])
        {
            var tempColor = levelButtons[2].color;
            tempColor.a = 0.5f;
            levelButtons[2].color = tempColor;
        }

        if (!_levelManager.GetUnlockedLevels[Level.InfiniteMode])
        {
            var tempColor = levelButtons[3].color;
            tempColor.a = 0.5f;
            levelButtons[3].color = tempColor;
        }
        
    }

    public void LoadDay1()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        if (!_levelManager.GetUnlockedLevels[Level.Day1]) return;
        SceneManager.LoadScene("Day1");
        _levelManager.SetCurrentLevel(Level.Day1);
    }

    public void LoadDay2()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        if (!_levelManager.GetUnlockedLevels[Level.Day2]) return;
        SceneManager.LoadScene("Day2");
        _levelManager.SetCurrentLevel(Level.Day2);
    }

    public void LoadDay3()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        if (!_levelManager.GetUnlockedLevels[Level.Day3]) return;
        SceneManager.LoadScene("Day3");
        _levelManager.SetCurrentLevel(Level.Day3);
    }

    public void LoadInfiniteMode()
    {
        //SceneManager.LoadScene("Game");
        // Should go to level select screen when it's finished.
        if (!_levelManager.GetUnlockedLevels[Level.InfiniteMode]) return;
        SceneManager.LoadScene("InfiniteMode");
        _levelManager.SetCurrentLevel(Level.InfiniteMode);
    }

    public void ShowNextLevel()
    {
        _currentPos++;

        if (_currentPos >= 0 && _currentPos <= 3)
        {
            _score.text = _levelManager.GetLevelScores[(Level)_currentPos].ToString();
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
            _score.text = _levelManager.GetLevelScores[(Level)_currentPos].ToString();
            _diasTransform.DOLocalMoveX(_levelPositions[_currentPos], 0.5f, true);
            _diasTransform.DOLocalRotate(new Vector3(720, 0, 0), 0.75f, RotateMode.FastBeyond360);
        }
        else
        {
            _currentPos++;
            _previousButtonTransform.DOShakePosition(0.5f, 10);
        }
    }

    public void PlayButtonSound()
    {
        _soundManager.PlayAudioClip(Sound.buttonClick);
    }
}
