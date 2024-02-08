using System;
 
using UnityEngine;
 
namespace My.Game.Scripts.Graphics
{
  public class FpsDisplay : MonoBehaviour
  {
    private int _1stGradeUpdateCounter;
    private int _2ndGradeUpdateCounter;
    private int _fpsCounter;
    private float _nextFpsTime;
 
    private void OnEnable()
    {
      this._nextFpsTime = Time.unscaledTime;
    }
 
    void Update()
    {
      var unscaledTime = Time.unscaledTime;
 
      if (unscaledTime > this._nextFpsTime)
      {
        this._fpsCounter = (this._1stGradeUpdateCounter + this._2ndGradeUpdateCounter);
 
        this._2ndGradeUpdateCounter = this._1stGradeUpdateCounter;
        this._1stGradeUpdateCounter = 1; // The current update always belongs to the next FPS counter label update
 
        this._nextFpsTime += 0.5f;
      }
      else
      {
        this._1stGradeUpdateCounter++;
      }
    }
 
    void OnGUI()
    {
      int w = Screen.width, h = Screen.height;
 
      GUIStyle style = new GUIStyle();
 
      // Change position when necessary
      Rect rect = new Rect(0, 0, w, h * 2 / 100);
      style.alignment = TextAnchor.UpperRight;
      style.fontSize = h * 2 / 100;
      // Change color when necessary
      style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
      GUI.Label(rect, this._fpsCounter.ToString(), style);
    }
  }
}