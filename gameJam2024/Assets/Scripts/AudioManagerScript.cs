using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    void Awake()
    {
        //Ensure no duplicates of AudioManager 
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //Ensure AudioManager persists from scene to scene
        DontDestroyOnLoad(gameObject);

        //Calls AudioManager component for each sound in array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //Play Main Theme
        //Play("CalmCosmos");
    }

    //Method to play sound. No error called if there is a typo.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }

    //Adding sounds: FindObjectOfType<AudioManagerScript>().Play("Jump");
}
