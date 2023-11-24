using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource BgMusic;
    private static Music instance = null;
    public static Music Instance => instance;

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
