using System;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;


    public Sounds[] musicSounds, sfxSounds;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("push");
    }

    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);
        if(s != null)
        {
            musicAudioSource.clip = s.clip;
            musicAudioSource.Play();
        }
        else
        {
            Debug.Log("Sound not found");
        }
    }

    public void PlaySfx(string name)
    {
        Sounds s = Array.Find(sfxSounds, x => x.name == name);

        if(s != null)
        {
            sfxAudioSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.Log("Sound effect not found");
        }
    }
}
