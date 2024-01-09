using System;
using UnityEngine;

public enum EAudioClips
{
    None = 0,
    astronautBgMusic = 1,
    collectFuel = 2,
    crash = 3,
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance { get; private set; }

    public bool isMuted = false;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
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
        Play(EAudioClips.astronautBgMusic);

        SetMusicListener();
    }

    public void Play(EAudioClips audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + audioName + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(EAudioClips audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + audioName + " not found");
            return;
        }
        
        s.source.Stop();
    }

    private void SetMusicListener()
    {
        isMuted = PlayerPrefs.GetInt("Muted") == 1;
        SetMusicState();
    }
    
    public void ToggleMusic()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        SetMusicState();
    }

    private void SetMusicState()
    {
        AudioListener.pause = isMuted;
    }
}
