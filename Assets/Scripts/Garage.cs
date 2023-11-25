using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PlayerCar;
    [SerializeField] private Sprite[] CarSprites;
    [SerializeField] private Text AccountBoard;
    private int AccountBalance = 0; 
    private int CurrentCar = 0;

    void Start()
    {
        AccountBalance = PlayerPrefs.GetInt("AccountBalance");
        AccountBoard.text = "AccountBalance: Rs " + AccountBalance; 
        
        ShowCar(CurrentCar);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowCar(CurrentCar + 1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowCar(CurrentCar - 1);
        }

        PlayerPrefs.SetInt("CurrentCar", CurrentCar);
    }

    private void ShowCar(int CarNo)
    {
        if(CarNo >= CarSprites.Length - 1)
        {
            CarNo = 0;
            CurrentCar = CarNo;
        }
        else if(CarNo < 0)
        {
            CarNo = CarSprites.Length - 1;
            CurrentCar = CarNo;
        }
        else
        {
            CurrentCar = CarNo;
        }

        PlayerCar.sprite = CarSprites[CurrentCar];
    }

    public void ShowNext()
    {
        ShowCar(CurrentCar + 1);
    }

    public void ShowPrevious()
    {
        ShowCar(CurrentCar - 1);
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
