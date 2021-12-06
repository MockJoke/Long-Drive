using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public int controls; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonsBtn()
    {
        controls = 1;
        PlayerPrefs.SetInt("controls", controls);
        SceneManager.LoadScene("Play");
    }

    public void KeysBtn()
    {
        controls = 2;
        PlayerPrefs.SetInt("controls", controls);
        SceneManager.LoadScene("Play");
    }

    public void TouchBtn()
    {
        controls = 3; 
        PlayerPrefs.SetInt("controls", controls);
        SceneManager.LoadScene("Play");
    }

    public void SensorBtn()
    {
        controls = 4;
        PlayerPrefs.SetInt("controls", controls);
        SceneManager.LoadScene("Play");
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
