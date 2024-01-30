using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Controls")]
    [SerializeField] private GameObject LeftTurnBtn;
    [SerializeField] private GameObject RightTurnBtn;
    [SerializeField] private GameObject accelerateBtn;
    [SerializeField] private GameObject decelerateBtn;
    private int currControls = 0;
    private int currCar = 0;
    private bool goingLeft = false;
    private bool goingRight = false;
    private bool carMoving = false;
    
    [Header("Movement")]
    [SerializeField] private float currSpeed = 4f;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationResetSpeed = 10f;
    [SerializeField] private float maxLeftBoundary = -2.2f;
    [SerializeField] private float maxRightBoundary = 2.2f;
    
    [Header("Info UI")]
    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private HealthBar healthBar;
    private int currHealth;
    private int score;
    private int highScore = 0;
    private int money;
    private int accountBalance;
    private float gameTime;
    
    [SerializeField] private SpriteRenderer PlayerCarImage; 
    [SerializeField] private Sprite[] PlayerCars;

    [Header("Managers")] 
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private ObjectSpawner objSpawner;
    [SerializeField] private Road road;

    #region Monobehaviour Methods
    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (PlayerCarImage == null)
            PlayerCarImage = GetComponent<SpriteRenderer>();

        if (objSpawner == null)
            objSpawner = FindObjectOfType<ObjectSpawner>();
        
        if (road == null)
            road = FindObjectOfType<Road>();
    }

    void Start()
    {
        currControls = PlayerPrefs.GetInt("controls", 2);

        currHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);

        currCar = PlayerPrefs.GetInt("CurrentCar", 0);
        accountBalance = PlayerPrefs.GetInt("AccountBalance", 0);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        PlayerCarImage.sprite = PlayerCars[currCar];
        
        road.SetSpeed(currSpeed);
        objSpawner.SetSpeed(currSpeed);
        
        UpdateControls();
    }

    void Update()
    {
        DetectInput();
        
        if (currHealth <= 0)
        {
            Time.timeScale = 0;

            gameplayUI.ToggleGameplayCanvas(false);
            gameplayUI.ToggleRetryCanvas(true);

            gameplayUI.UpdateScore(score);
            CheckHighScore();
            
            money = score / 5;
            gameplayUI.UpdateMoney(money);
            
            accountBalance += money;
            PlayerPrefs.SetInt("AccountBalance", accountBalance);
        }

        Move();

        gameTime += Time.deltaTime;
        score = (int)gameTime;
    }
    
    void OnDestroy()
    {
        healthBar.SetHealth(MaxHealth);
        Time.timeScale = 1;
    }
    #endregion
    
    private void UpdateControls()
    {
        switch (currControls)
        {
            case 1:
                LeftTurnBtn.SetActive(true);
                RightTurnBtn.SetActive(true);
                accelerateBtn.SetActive(true);
                decelerateBtn.SetActive(true);
                break;
            case 2:
                LeftTurnBtn.SetActive(false);
                RightTurnBtn.SetActive(false);
                accelerateBtn.SetActive(false);
                decelerateBtn.SetActive(false);
                break;
            case 3:
            case 4:
                LeftTurnBtn.SetActive(false);
                RightTurnBtn.SetActive(false);
                accelerateBtn.SetActive(true);
                decelerateBtn.SetActive(true);
                break;
        }
    }

    private void DetectInput()
    {
        switch (currControls)
        {
            case 2:
                MoveOnKeys();
                break;
            case 3:
                MoveOnTouch();
                break;
            case 4:
                MoveOnSensor();
                break;
        }
    }

    private void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        
        gameplayUI.UpdateHighScore(highScore);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score--;
            PlayerPrefs.SetInt("score", score); 
            currHealth -= 2;
            healthBar.SetHealth(currHealth);
            
            AudioManager.instance.Play(EAudioClips.crash);

            if(currHealth != 0)
            {
                Destroy(other.gameObject); 
            }
        }

        if (other.gameObject.CompareTag("Fuel"))
        {
            if(currHealth < MaxHealth)
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

            Destroy(other.gameObject);
        }
    }
    
    public void RetryBtn()
    {
        gameplayUI.ToggleRetryCanvas(true);
        gameplayUI.ToggleGameplayCanvas(false);
        
        SceneManager.LoadScene("Play");

        gameTime = 0;
    }

    private void Move()
    {
        if(goingLeft)
        {
            LeftSide();
        }
        else if(goingRight)
        {
            RightSide(); 
        }

        // if (transform.position.x < maxLeftBoundary)
        // {
        //     transform.position = new Vector3(maxLeftBoundary, transform.position.y, transform.position.z);
        // }
        // if (transform.position.x > maxRightBoundary)
        // {
        //     transform.position = new Vector3(maxRightBoundary, transform.position.y, transform.position.z);
        // }
        
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, maxLeftBoundary, maxRightBoundary);
        transform.position = pos;
        
        if(carMoving)
        {
            RotateCar();
        }
        else
        {
            ResetCarRotation(); 
        }
    }
    
    private void LeftSide()
    {
        transform.position -= new Vector3(currSpeed * Time.deltaTime, 0, 0);
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -maxRotationAngle), rotationSpeed * Time.deltaTime);
        
        // transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, 0);
        //MainCar.velocity = Vector2.left;
    }

    private void RightSide()
    {
        transform.position += new Vector3(currSpeed * Time.deltaTime, 0, 0);
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, maxRotationAngle), rotationSpeed * Time.deltaTime);
        
        // transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, 0);
        //MainCar.velocity = Vector2.right;
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            carMoving = true;
            goingLeft = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            carMoving = true;
            goingRight = true;
        }
        else
        {
            carMoving = false;
            goingLeft = false;
            goingRight = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            AccelerateCar();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            DecelerateCar();
        }
    }

    private void MoveOnSensor()
    {
        if(Input.acceleration.x < -0.1f)
        {
            carMoving = true;
            goingLeft = true;
        }
        else if(Input.acceleration.x > 0.1f)
        {
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
                carMoving = true;
                goingLeft = true; 
            }
            else
            {
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, maxRotationAngle), rotationSpeed * Time.deltaTime);
            
            // carRotation += Time.deltaTime * rotationSpeed;    //rotation var set on pressing left side btn  
            // transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(carRotation, 0f, maxRotationAngle));   //pos transformed to rotate
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -maxRotationAngle), rotationSpeed * Time.deltaTime);
            
            // carRotation -= Time.deltaTime * rotationSpeed;
            // transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(carRotation, -maxRotationAngle, 0f));   //pos transformed to rotate
        }
    }

    private void ResetCarRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationResetSpeed * Time.deltaTime);
        
        // carRotation = 0f;
        // transform.rotation = Quaternion.Euler(0, 0, carRotation);
    }
    
    public void AccelerateCar()
    {
        currSpeed += 0.01f;
        currSpeed = Mathf.Clamp(currSpeed, minSpeed, maxSpeed);
        
        objSpawner.SetSpeed(currSpeed);
        road.SetSpeed(currSpeed);
    }

    public void DecelerateCar()
    {
        currSpeed -= 0.01f;
        currSpeed = Mathf.Clamp(currSpeed, minSpeed, maxSpeed);

        objSpawner.SetSpeed(currSpeed);
        road.SetSpeed(currSpeed);
    }
}
