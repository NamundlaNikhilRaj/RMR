using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    
    private static musicManager instance = null;
    private AudioSource musicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the GameObject from being destroyed when changing scenes
            musicSource = GetComponent<AudioSource>();
            musicSource.Play(); // Play the music
        }
        else
        {
            Destroy(gameObject); // Ensures that only one instance of the music exists
        }
    }
}
