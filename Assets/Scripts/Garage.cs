using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    public GameObject[] CarList;
    public Text AccountBoard; 
    public int AccountBalance, CurrentCar = 0;

    // Start is called before the first frame update
    void Start()
    {
        AccountBalance = PlayerPrefs.GetInt("AccountBalance");
        AccountBoard.text = "AccountBalance: Rs " + AccountBalance; 
 
        foreach(GameObject ChosenCar in CarList)
        {
            
            ChosenCar.SetActive(false); 
        }

        ShowCar(CurrentCar); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentCar++;
            ShowCar(CurrentCar);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentCar--;
            ShowCar(CurrentCar);
        }

        PlayerPrefs.SetInt("CurrentCar", CurrentCar);

    }

    public void ShowCar(int CarNo)
    {
        if(CarNo >= CarList.Length)
        {
            CarNo = 0;
            CurrentCar = CarNo;
        }
        else if(CarNo < 0)
        {
            CarNo = CarList.Length;
            CurrentCar = CarNo;

        }

        foreach (GameObject ChosenCar in CarList)
        {
            ChosenCar.SetActive(false);
        }

        CarList[CarNo].SetActive(true);

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
