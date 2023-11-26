using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D MainCar;
    [SerializeField] private GameObject GameplayCanvas; 
    [SerializeField] private GameObject RetryCanvas;

    [Header("Controls")] 
    [SerializeField] private GameObject LeftTurnBtn; 
    [SerializeField] private GameObject RightTurnBtn;
    private int currControls = 0; 
    private int currCar = 0;
    private bool goingLeft = false;
    private bool goingRight = false; 
    private bool carMoving = false;
    
    private float carRotation = 0f;
    
    [Header("Info UI")]
    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TextMeshProUGUI ScoreBoard; 
    [SerializeField] private TextMeshProUGUI MoneyReceived; 
    private int currHealth;
    private int score, money, accountBalance, speed; 
    private float GameTime;
    
    [SerializeField] private SpriteRenderer CarImage; 
    [SerializeField] private Sprite[] PlayerCars;

    void Start()
    {
        
        if (MainCar == null)
            MainCar = GetComponent<Rigidbody2D>();
        
        if (CarImage == null)
            CarImage = GetComponent<SpriteRenderer>();
        
        currControls = PlayerPrefs.GetInt("controls");

        currHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

        currCar = PlayerPrefs.GetInt("CurrentCar");
        accountBalance = PlayerPrefs.GetInt("AccountBalance", 0);
    }

    void Update()
    {
        CarImage.sprite = PlayerCars[currCar];

        switch (currControls)
        {
            case 1:
                LeftTurnBtn.SetActive(true);
                RightTurnBtn.SetActive(true);
                break;
            case 2:
                MoveOnKeys(); 
                LeftTurnBtn.SetActive(false);
                RightTurnBtn.SetActive(false);
                break;
            case 3:
                MoveOnTouch();
                LeftTurnBtn.SetActive(false);
                RightTurnBtn.SetActive(false);
                break;
            default:
                MoveOnSensor(); 
                LeftTurnBtn.SetActive(false);
                RightTurnBtn.SetActive(false);
                break;
        }

        if (currHealth == 0)
        {
            Time.timeScale = 0; //to make game into pause mode 
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in Enemies)
            {
                enemy.GetComponent<MoveObj>().speed = 0f;  //to stop already generated enemy cars by making its speed 0
            }

            GameplayCanvas.GetComponent<CanvasGroup>().interactable = false;

            RetryCanvas.SetActive(true);

            ScoreBoard.text = "SCORE: " + score;

            money = score / 5;
            MoneyReceived.text = "Money: Rs" + money;
        }

        if(goingLeft)
        {
            LeftSide();
        }

        if(goingRight)
        {
            RightSide(); 
        }

        if(carMoving)
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
        accountBalance = accountBalance + money;
        PlayerPrefs.SetInt("AccountBalance", accountBalance);
        RetryCanvas.SetActive(false);

        GameplayCanvas.GetComponent<CanvasGroup>().interactable = true;
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
            currHealth -= 2;
            healthBar.SetHealth(currHealth);
            
            AudioManager.instance.Play(EAudioClips.crash);

            if(currHealth != 0)
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
            if(currHealth<MaxHealth)
            {
                currHealth += 1;
                healthBar.SetHealth(currHealth);
            }
            else
            {
                currHealth = MaxHealth;
                healthBar.SetHealth(currHealth);
            }

            AudioManager.instance.Play(EAudioClips.collectFuel);

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
        goingLeft = true;
        carMoving = true; 
    }

    public void LeftBtnUp()
    {
        goingLeft = false;
        carMoving = false; 
    }

    public void RightBtnDown()
    {
        goingRight = true;
        carMoving = true; 
    }

    public void RightBtnUp()
    {
        goingRight = false;
        carMoving = false; 
    }

    private void MoveOnKeys()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftSide();
            carMoving = true;
            goingLeft = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RightSide();
            carMoving = true;
            goingRight = true;
        }
        else
        {
            carMoving = false;
            goingLeft = false;
            goingRight = false;
        }
    }

    private void MoveOnSensor()
    {
        if(Input.acceleration.x < -0.1f)
        {
            LeftSide();
            carMoving = true;
            goingLeft = true;
        }
        else if(Input.acceleration.x > 0.1f)
        {
            RightSide();
            carMoving = true;
            goingRight = true;
        }
        else
        {
            carMoving = false;
            goingLeft = false;
            goingRight = false;
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
                carMoving = true;
                goingLeft = true; 
            }
            else
            {
                RightSide();
                carMoving = true;
                goingRight = true; 
            }
        }
        else
        {
            carMoving = false;
            goingLeft = false;
            goingRight = false; 
        }
    }

    private void RotateCar()
    {
        if(goingLeft)
        {
            carRotation += Time.deltaTime * 10f;    //rotation var set on pressing left side btn  
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(carRotation,0f,7f));   //pos transformed to rotate
        }
        else
        {
            carRotation -= Time.deltaTime * 10f;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(carRotation,-7f,0f));   //pos transformed to rotate
        }
    }

    private void ResetCarRotation()
    {
        carRotation = 0f;
        transform.rotation = Quaternion.Euler(0, 0, carRotation);
    }
}
