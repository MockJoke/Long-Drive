using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private int controls = 1;
    public Action OnClose;

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

    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Home");
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
    
    public void MusicBtn()
    {
        AudioManager.instance.ToggleMusic();
    }
}
