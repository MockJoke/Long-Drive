using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private Canvas GameplayCanvas;
    [SerializeField] private Canvas RetryCanvas;
    [SerializeField] private Canvas PauseCanvas;
    [SerializeField] private SettingsManager settingsManager;
    
    [Header("Fields")]
    [SerializeField] private TextMeshProUGUI ScoreBoard; 
    [SerializeField] private TextMeshProUGUI MoneyReceived;

    private void Awake()
    {
        if (settingsManager == null)
            settingsManager = FindObjectOfType<SettingsManager>();
    }

    public void UpdateScore(int value)
    {
        ScoreBoard.text = "SCORE: " + value;
    }

    public void UpdateMoney(int value)
    {
        MoneyReceived.text = "Money: Rs" + value;
    }

    public void ToggleRetryCanvas(bool b)
    {
        RetryCanvas.enabled = b;
    }

    public void ToggleGameplayCanvas(bool b)
    {
        GameplayCanvas.enabled = b;
    }

    private void TogglePauseCanvas(bool b)
    {
        PauseCanvas.enabled = b;
    }

    public void SettingsBtn()
    {
        Time.timeScale = 0;
        ToggleGameplayCanvas(false);
        settingsManager.OpenMenu();
        settingsManager.OnClose += ReopenGameplayCanvas;
    }

    public void PauseBtn()
    {
        Time.timeScale = 0;
        TogglePauseCanvas(true);
    }
    
    public void ResumeBtn()
    {
        TogglePauseCanvas(false);
        ToggleGameplayCanvas(true);
        Time.timeScale = 1;
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Home");
    }
    
    public void QuitBtn()
    {
#if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
    }
    
    private void ReopenGameplayCanvas()
    {
        ToggleGameplayCanvas(true);
        settingsManager.ResetCloseAction();
        Time.timeScale = 1;
    }
}
