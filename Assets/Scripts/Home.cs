using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    }
}
