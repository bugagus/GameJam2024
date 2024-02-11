using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AlwaysSound : MonoBehaviour
{
    public static AlwaysSound instance;
 
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

}
