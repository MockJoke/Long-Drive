﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PlayerCar;
    [SerializeField] private Sprite[] CarSprites;
    [SerializeField] private TextMeshProUGUI Money;
    private int accountBalance = 0;
    private int currCar = 0;

    void Start()
    {
        accountBalance = PlayerPrefs.GetInt("AccountBalance");
        Money.text = $"{accountBalance}"; 
        
        ShowCar(currCar);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ShowCar(currCar + 1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ShowCar(currCar - 1);
        }
    }

    private void ShowCar(int CarNo)
    {
        if(CarNo > CarSprites.Length - 1)
        {
            CarNo = 0;
        }
        else if(CarNo < 0)
        {
            CarNo = CarSprites.Length - 1;
        } 
        
        currCar = CarNo;

        PlayerCar.sprite = CarSprites[currCar];
        
        PlayerPrefs.SetInt("CurrentCar", currCar);
    }

    public void ShowNext()
    {
        ShowCar(currCar + 1);
    }

    public void ShowPrevious()
    {
        ShowCar(currCar - 1);
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
