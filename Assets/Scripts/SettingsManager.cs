﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyUI.Toast;

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
        Toast.Show ("Controls have been set to <b><color=black>Buttons</color></b>", 3f, ToastColor.Green);
    }

    public void KeysBtn()
    {
        controls = 2;
        PlayerPrefs.SetInt("controls", controls);
        Toast.Show ("Controls have been set to <b><color=black>Keys</color></b>", 3f, ToastColor.Green);
    }

    public void TouchBtn()
    {
        controls = 3; 
        PlayerPrefs.SetInt("controls", controls);
        Toast.Show ("Controls have been set to <b><color=black>Touch</color></b>", 3f, ToastColor.Green);
    }

    public void SensorBtn()
    {
        controls = 4;
        PlayerPrefs.SetInt("controls", controls);
        Toast.Show ("Controls have been set to <b><color=black>Sensors</color></b>", 3f, ToastColor.Green);
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
