using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject redBall2Ref;  
    public GameObject redBallRef;

    public GameObject restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeLeftText;

    private float startTimer;
    private bool timerStarted = false;
    private bool sceneChanged = false;

    private float secondSceneTimer;
    private bool secondSceneTimerStarted = false;

    public int highScore;
    public int score = 0;
    public bool over;

    private bool powerUpSpawned = false;
    public GameObject powerUpRef;
    public delegate void PowerUp();
    public static PowerUp PoweredUp;    

    public delegate void disableRb();
    public static disableRb disabledRb;


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
        highScore = PlayerPrefs.GetInt("highscore", 0);

    }


    private void Update()
    {

        if (timerStarted && !sceneChanged && !over)
        {

            int timer = Mathf.RoundToInt(Time.time - startTimer);
            scoreText.text = "Score: " + score;

            if (timer >= 30f) 
            {

                sceneChanged = true;
                SceneManager.LoadScene("Scene2");

            }

            if (timer == 10f && !powerUpSpawned)
            {

                SpawnPowerUp();

            }

            timeLeftText.text = (30 - timer).ToString();

        }

        if (secondSceneTimerStarted)
        {

            secondSceneTimer += Time.deltaTime;
            timeLeftText.text = (30 - Mathf.RoundToInt(secondSceneTimer)).ToString();

            if (secondSceneTimer > 30f) 
            {

                secondSceneTimerStarted = false;
                SceneManager.LoadScene("FinishedScene");

            }

        }

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
            greenBall.Spawn();
            SpawnRedBall();

            secondSceneTimer = 0f; 
            secondSceneTimerStarted = true;

        }

        if (scene.name == "MainScene")
        {

            GreenBall greenBall = FindObjectOfType<GreenBall>();
            greenBall.Spawn();
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
        scoreText.enabled = true;
        timeLeftText.enabled = true;

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
        secondSceneTimerStarted = false;
        restartButton.SetActive(true);
        gameOverText.text = "Game OVER!";

        if (score > PlayerPrefs.GetInt("highscore"))
        {

            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            gameOverText.text = gameOverText.text + "\nNEW HIGHSCORE: " + score;

        }

        gameOverText.gameObject.SetActive(true);

        if (disabledRb != null)
        {
            disabledRb();
        }

    }


    public void OnPlayerTouchRedBall()
    {
        if (!over)
        {
            GameOver();
        }
       

    }


    public void Restart()
    {
        
        SceneManager.LoadScene("MainScene");
        ResetGame();

    }


    public void ResetGame()
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
