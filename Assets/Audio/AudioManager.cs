using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public LevelManager levelManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    private void Update()
    {
        if(levelManager.health <= 0)
        {
            StopMusic();
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }

        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
            Debug.Log("Playing Music.");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }

        else
        {
            sfxSource.clip = s.audioClip;
            sfxSource.PlayOneShot(s.audioClip);
            Debug.Log("Playing Sound.");
        }
    }

    public void StopMusic()
    {
        if(musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
