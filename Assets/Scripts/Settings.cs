using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private int controls;

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
