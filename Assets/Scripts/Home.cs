using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject homeCanvasObj;
    [SerializeField] private SettingsManager settingsManager;

    void Awake()
    {
        if (settingsManager == null)
            settingsManager = FindObjectOfType<SettingsManager>();
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void GarageBtn()
    {
        SceneManager.LoadScene("Garage");
    }
    
    public void RoadSelectionBtn()
    {
        SceneManager.LoadScene("RoadSelection");
    }

    public void SettingsBtn()
    {
        homeCanvasObj.SetActive(false);
        settingsManager.OpenMenu();
        settingsManager.OnClose += OpenHomeCanvas;
    }

    private void OpenHomeCanvas()
    {
        homeCanvasObj.SetActive(true);
        settingsManager.ResetCloseAction();
    }

    public void QuitBtn()
    {
#if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
    }
}
