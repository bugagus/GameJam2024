using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    [SerializeField] AudioClip[] musics;
    private AudioSource audioSource;
 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayMusicGame()
    {
        audioSource.volume = 0.06f;
        GetComponent<AudioSource>().clip = musics[1];
        GetComponent<AudioSource>().Play();
    }

    public void PlayMusicOutGame()
    {
        audioSource.volume = 0.1f;
        GetComponent<AudioSource>().clip = musics[0];
        GetComponent<AudioSource>().Play();
    }

}
