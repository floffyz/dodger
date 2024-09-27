using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public GreenBall greenBall;
    public RedBall redBall;

    public GameObject restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;


    public int score = 0;
    public bool over;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        over = false;
        restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        greenBall.Spawn();
        redBall.Spawn();
    }

    private void Update()
    {
        // Atualiza o texto de pontuação
        scoreText.text = "Score: " + score;
    }

    public void Score(GreenBall greenBall)
    {
        score++;
        StartCoroutine(greenBall.Respawn());
    }

    public void GameOver()
    {
        over = true;
        restartButton.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void OnPlayerTouchRedBall()
    {
        GameOver();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
