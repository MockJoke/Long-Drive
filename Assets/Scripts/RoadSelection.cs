using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoadSelection : MonoBehaviour
{
    [SerializeField] private Image RoadImg;
    [SerializeField] private Sprite[] RoadSprites;
    [SerializeField] private TextMeshProUGUI Money;
    private int accountBalance = 0;
    private int currRoad = 0;

    void Start()
    {
        accountBalance = PlayerPrefs.GetInt("AccountBalance");
        Money.text = $"{accountBalance}"; 
        
        ShowRoad(currRoad);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowRoad(currRoad + 1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowRoad(currRoad - 1);
        }
    }

    private void ShowRoad(int RoadNo)
    {
        if(RoadNo > RoadSprites.Length - 1)
        {
            RoadNo = 0;
        }
        else if(RoadNo < 0)
        {
            RoadNo = RoadSprites.Length - 1;
        } 
        
        currRoad = RoadNo;

        RoadImg.sprite = RoadSprites[currRoad];
        
        PlayerPrefs.SetInt("CurrentRoad", currRoad);
    }

    public void ShowNext()
    {
        ShowRoad(currRoad + 1);
    }

    public void ShowPrevious()
    {
        ShowRoad(currRoad - 1);
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
