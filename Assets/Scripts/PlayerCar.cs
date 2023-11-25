using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D MainCar;
    [SerializeField] private GameObject can, RetryObj;
    private int CurrentHealth; 
    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private GameObject LeftBtn, RightBtn; 
    private bool GoingLeft = false, GoingRight = false, CarMoving = false;
    [SerializeField] private int controls, CurrentCar; 
    private float CarRotation = 0f;
    [SerializeField] private AudioSource sounds;
    [SerializeField] private AudioClip CarCrash, ReFuel;
    [SerializeField] private Sprite[] PlayerCars;
    private int score, money, AccountBalance, speed; 
    private float GameTime;
    [SerializeField] private Text ScoreBoard, MoneyReceived; 
    
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private SpriteRenderer CarImage; 

    //public AudioSource Audio; 
    //public AudioClip CollisionSound, 

    void Start()
    {
        if (sounds == null)
            sounds = GetComponent<AudioSource>();
        
        if (MainCar == null)
            MainCar = GetComponent<Rigidbody2D>();
        
        if (CarImage == null)
            CarImage = GetComponent<SpriteRenderer>();
        
        controls = PlayerPrefs.GetInt("controls");

        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

        CurrentCar = PlayerPrefs.GetInt("CurrentCar");
        AccountBalance = PlayerPrefs.GetInt("AccountBalance", 0);
    }

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
            MoneyReceived.text = "Money: Rs" + money;
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
        // GameTime = (int)Time.time; 
        score = (int)GameTime;
        
        // AccountBalance += money;
        // PlayerPrefs.SetInt("AccountBalance", AccountBalance);

        // print(Mathf.FloorToInt(GameTime));
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

    private void LeftSide()
    {
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
        }
            
        //MainCar.velocity = Vector2.left;
    }

    private void RightSide()
    {
        //if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        }
            
        //MainCar.velocity = Vector2.right;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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

        if (collision.gameObject.CompareTag("Fuel"))
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

    private void MoveOnKeys()
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

    private void MoveOnSensor()
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

    private void MoveOnTouch()
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

    private void RotateCar()
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

    private void ResetCarRotation()
    {
        CarRotation = 0f;
        transform.rotation = Quaternion.Euler(0, 0, CarRotation);
    }
}
