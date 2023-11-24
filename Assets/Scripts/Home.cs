using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void GarageBtn()
    {
        SceneManager.LoadScene("Garage");
    }

    public void SettingsBtn()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
