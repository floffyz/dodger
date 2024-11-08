using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject redBall2Ref;  
    public GameObject redBallRef;

    public GameObject powerUpRef;

    public GameObject restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeLeftText;

    public int highScore;


    private float startTimer;
    private bool timerStarted = false;
    private bool sceneChanged = false;

    private bool powerUpSpawned = false;

    public int score = 0;
    public bool over;

    private float secondSceneTimer;
    private bool secondSceneTimerStarted = false;


    public delegate void PowerUp();
    public static PowerUp PoweredUp;    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        StartGame();
    }




    private void Start()
    {
        over = false;
        restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        //newHighScore = false;
        highScore = PlayerPrefs.GetInt("highscore", 0);


    }

    private void Update()
    {

       
        if (timerStarted && !sceneChanged && !over)
        {
            int timer = Mathf.RoundToInt(Time.time - startTimer);
            scoreText.text = "Score: " + score;

            if (timer >= 5f) 
            {
                sceneChanged = true;
                SceneManager.LoadScene("Scene2");
            }

            if (timer == 5f && !powerUpSpawned)
            {
                SpawnPowerUp();
            }



            timeLeftText.text = "timeleft " + (30 - timer);


        }

        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.SetInt("highscore", 0);
            highScore = 0;
        }


        if (secondSceneTimerStarted)
        {
            secondSceneTimer += Time.deltaTime;
            timeLeftText.text = "timeleft " + (30 - Mathf.RoundToInt(secondSceneTimer));
            if (secondSceneTimer > 10f) 
            {
                secondSceneTimerStarted = false;
                SceneManager.LoadScene("FinishedScene");
            }
        }


        print(highScore);




    }

    private void SpawnPowerUp()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        GameObject spawnedPowerUp;
        spawnedPowerUp = Instantiate(powerUpRef, spawnPosition, Quaternion.identity);
        spawnedPowerUp.transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));

        powerUpSpawned = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene2")
        {
            GreenBall greenBall = FindObjectOfType<GreenBall>();
            greenBall?.Spawn();

           
            SpawnRedBall();

            secondSceneTimer = 0f; 
            secondSceneTimerStarted = true;
        }

        if (scene.name == "MainScene")
        {
            GreenBall greenBall = FindObjectOfType<GreenBall>();
            greenBall?.Spawn();

            SpawnRedBall();
        }
    }



    private void SpawnRedBall()
    {
       

            Vector3 spawnPosition = new Vector3(0, 0, 0); 



          
            GameObject spawnedRedBall;
            if (Random.Range(0, 2) == 0)
            {
                spawnedRedBall = Instantiate(redBall2Ref, spawnPosition, Quaternion.identity);
               
            }
            else
            {
                spawnedRedBall = Instantiate(redBallRef, spawnPosition, Quaternion.identity);

            }
               

       
    }

    public void StartGame()
    {
        startTimer = Time.time;
        timerStarted = true;
        sceneChanged = false;
        over = false;
        score = 0;
    }

    public void Score(GreenBall greenBall)
    {
        score++;
        scoreText.text = "Score: " + score; 
        StartCoroutine(greenBall.Respawn());
    }

    public void GameOver()
    {
        over = true;
        timerStarted = false; 
        restartButton.SetActive(true);
        gameOverText.text = "GAME OVER!";
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            gameOverText.text = gameOverText.text + "\nNEW HIGHSCORE: " + score;
        }
        gameOverText.gameObject.SetActive(true);
    }

    public void OnPlayerTouchRedBall()
    {
        GameOver();
    }

    public void Restart()
    {
        
        SceneManager.LoadScene("MainScene");
       
        ResetGame();
    }

    private void ResetGame()
    {
       
        StartGame();
        powerUpSpawned = false;
        restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
