using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    public Rigidbody2D MainCar;
    public GameObject can, RetryObj;
    public int CurrentHealth, MaxHealth = 10;
    public GameObject LeftBtn, RightBtn; 
    bool GoingLeft = false, GoingRight = false, CarMoving = false;
    public int controls, CurrentCar; 
    float CarRotation = 0f;
    AudioSource sounds;
    public AudioClip CarCrash, ReFuel;
    public Sprite[] PlayerCars;
    public int score, money, AccountBalance, speed; 
    public float GameTime;
    public Text ScoreBoard, MoneyRecieved; 

    public SpriteRenderer CarImage; 

    //public AudioSource Audio; 
    //public AudioClip CollisionSound, 

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();

        MainCar = GetComponent<Rigidbody2D>();

        CarImage = GetComponent<SpriteRenderer>();

        controls = PlayerPrefs.GetInt("controls");

        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

        CurrentCar = PlayerPrefs.GetInt("CurrentCar");
        AccountBalance = PlayerPrefs.GetInt("AccountBalance", 0);
    }

    public healthbar healthBar; 

    // Update is called once per frame
    void Update()
    {
        CarImage.sprite = PlayerCars[CurrentCar];

        if (controls == 1)
        {
            LeftBtn.SetActive(true);
            RightBtn.SetActive(true);
        }

        else if (controls == 2)
        {
            MoveOnKeys(); 
            LeftBtn.SetActive(false);
            RightBtn.SetActive(false);
        }

        else if (controls == 3)
        {
            MoveOnTouch();
            LeftBtn.SetActive(false);
            RightBtn.SetActive(false);
        }

        else
        {
            MoveOnSensor(); 
            LeftBtn.SetActive(false);
            RightBtn.SetActive(false);
        }

        if (CurrentHealth == 0)
        {
            Time.timeScale = 0; //to make game into pause mode 
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in Enemies)
            {
                enemy.GetComponent<MoveObj>().speed = 0f;  //to stop already generated enemy cars by making its speed 0
            }

            can.GetComponent<CanvasGroup>().interactable = false;

            RetryObj.SetActive(true);

            ScoreBoard.text = "SCORE: " + score;

            money = score / 5;
            MoneyRecieved.text = "Money: Rs" + money;

        }

        if(GoingLeft)
        {
            LeftSide();
        }

        if(GoingRight)
        {
            RightSide(); 
        }

        if(CarMoving)
        {
            RotateCar();
        }
        else
        {
            ResetCarRotation(); 
        }

        GameTime += Time.deltaTime;
        //GameTime = (int)Time.time; 
        score = (int)GameTime;
        
        
        //AccountBalance += money;
        //PlayerPrefs.SetInt("AccountBalance", AccountBalance);

        print(Mathf.FloorToInt(GameTime));
        //print(Time.deltaTime); 
        
    }

    public void RetryBtn()
    {
        AccountBalance = AccountBalance + money;
        PlayerPrefs.SetInt("AccountBalance", AccountBalance);
        RetryObj.SetActive(false);

        can.GetComponent<CanvasGroup>().interactable = true;
        SceneManager.LoadScene("Play");

        GameTime = 0;
        

    }

    public void SettingsBtn()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LeftSide()
    {
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
        }
            
        //MainCar.velocity = Vector2.left;
    }

    public void RightSide()
    {
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        }
            
        //MainCar.velocity = Vector2.right;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            score--;
            PlayerPrefs.SetInt("score", score); 
            CurrentHealth -= 2;
            healthBar.SetHealth(CurrentHealth);
            sounds.clip = CarCrash;
            sounds.Play(); 

            if(CurrentHealth != 0)
            {
                Destroy(collision.gameObject); 
            }
            

            //Time.timeScale = 0; //to make game into pause mode 
            //GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (GameObject enemy in Enemies)
            //{
            //    enemy.GetComponent<MoveObj>().speed = 0f;  //to stop already generated enemy cars by making its speed 0
            //}
        }

        if (collision.gameObject.tag == "Fuel")
        {
            if(CurrentHealth<MaxHealth)
            {
                CurrentHealth += 1;
                healthBar.SetHealth(CurrentHealth);
            }
            else
            {
                CurrentHealth = MaxHealth;
                healthBar.SetHealth(CurrentHealth);
            }

            sounds.clip = ReFuel;
            sounds.Play(); 

            Destroy(collision.gameObject);
        }

    }

    private void OnDestroy()
    {
        healthBar.SetHealth(MaxHealth);
        Time.timeScale = 1;
    }

    public void LeftBtnDown()
    {
        GoingLeft = true;
        CarMoving = true; 
    }

    public void LeftBtnUp()
    {
        GoingLeft = false;
        CarMoving = false; 
    }

    public void RightBtnDown()
    {
        GoingRight = true;
        CarMoving = true; 
    }

    public void RightBtnUp()
    {
        GoingRight = false;
        CarMoving = false; 
    }

    public void MoveOnKeys()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftSide();
            CarMoving = true;
            GoingLeft = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RightSide();
            CarMoving = true;
            GoingRight = true;
        }
        else
        {
            CarMoving = false;
            GoingLeft = false;
            GoingRight = false;
        }

    }
    public void MoveOnSensor()
    {
        if(Input.acceleration.x < -0.1f)
        {
            LeftSide();
            CarMoving = true;
            GoingLeft = true;
        }
        else if(Input.acceleration.x > 0.1f)
        {
            RightSide();
            CarMoving = true;
            GoingRight = true;
        }
        else
        {
            CarMoving = false;
            GoingLeft = false;
            GoingRight = false;
        }
    }

    public void MoveOnTouch()
    {
        if(Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;   //get the position of the first touch on screen 
            float divider = Screen.width / 2; 
            
            if(pos.x < divider)
            {
                LeftSide();
                CarMoving = true;
                GoingLeft = true; 
            }
            else
            {
                RightSide();
                CarMoving = true;
                GoingRight = true; 
            }
        }
        else
        {
            CarMoving = false;
            GoingLeft = false;
            GoingRight = false; 
        }
        

    }

    public void RotateCar()
    {
        if(GoingLeft)
        {
            CarRotation += Time.deltaTime * 10f;    //rotation var set on pressing left side btn  
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(CarRotation,0f,7f));   //pos transformed to rotate
        }
        else
        {
            CarRotation -= Time.deltaTime * 10f;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(CarRotation,-7f,0f));   //pos transformed to rotate
        }

          
    }

    public void ResetCarRotation()
    {
        CarRotation = 0f;
        transform.rotation = Quaternion.Euler(0, 0, CarRotation);
    }    

}
