using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
   [SerializeField] private AudioMixer audioMixer;

   public void MusicController(float musicSlider)
   {
    audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider) * 20);
   }

   private void Awake()
   {
    DontDestroyOnLoad(transform.gameObject);
   }
}
