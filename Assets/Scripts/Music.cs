using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Music : MonoBehaviour
{
    AudioSource BgMusic;
    private static Music instance = null;
    public static Music Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        BgMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BgMusicSwitch(bool value)
    {
        if(value)
        {
            BgMusic.Play(); 
        }
        else
        {
            BgMusic.Stop();
        }    
    }
}
