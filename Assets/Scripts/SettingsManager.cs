using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle MusicToggle;
    private int controls = 1;
    public Action OnClose;

    void Start()
    {
        SetMusicToggle();
    }

    public void ButtonsBtn()
    {
        controls = 1;
        PlayerPrefs.SetInt("controls", controls);
    }

    public void KeysBtn()
    {
        controls = 2;
        PlayerPrefs.SetInt("controls", controls);
    }

    public void TouchBtn()
    {
        controls = 3; 
        PlayerPrefs.SetInt("controls", controls);
    }

    public void SensorBtn()
    {
        controls = 4;
        PlayerPrefs.SetInt("controls", controls);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
    
    public void ResetCloseAction()
    {
        OnClose = null;
    }

    private void SetMusicToggle()
    {
        MusicToggle.isOn = PlayerPrefs.GetInt("Muted") != 1;
    }
    
    public void MusicBtn()
    {
        AudioManager.instance.ToggleMusic();
    }
    
    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Home");
    }
}
