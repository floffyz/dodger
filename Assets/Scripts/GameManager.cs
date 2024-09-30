using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GreenBall greenBall; // Referência da GreenBall

    public GameObject redBall2Ref;  // Referência para a bola vermelha alternativa
    public GameObject redBallRef;   // Referência para a bola vermelha

    public GameObject restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    private float startTimer;
    private bool timerStarted = false;
    private bool sceneChanged = false;

    public int score = 0;
    public bool over;

    // Variáveis para o segundo timer na cena 2
    private float secondSceneTimer;
    private bool secondSceneTimerStarted = false;

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

        // Spawnar a GreenBall na cena inicial
        greenBall?.Spawn();

        // Spawnar a bola vermelha ao iniciar o jogo
        SpawnRedBall();
    }

    private void Update()
    {
        // Somente conte o tempo se o jogo não estiver acabado
        if (timerStarted && !sceneChanged && !over)
        {
            int timer = Mathf.RoundToInt(Time.time - startTimer);
            scoreText.text = "Score: " + score;

            if (timer >= 6) // Alterar para 30 segundos conforme solicitado
            {
                sceneChanged = true;
                SceneManager.LoadScene("Scene2");
            }
        }

        // Lógica para o segundo timer na cena 2
        if (secondSceneTimerStarted)
        {
            secondSceneTimer += Time.deltaTime; // Aumenta o timer com o tempo

            if (secondSceneTimer >= 7f) // Após 30 segundos
            {
                Debug.Log("30 seconds passed in Scene 2, changing to FinishedScene...");
                SceneManager.LoadScene("FinishedScene");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene2")
        {
            greenBall = FindObjectOfType<GreenBall>();
            greenBall?.Spawn();

            // Tentar spawnar a bola vermelha novamente
            Debug.Log("Cena 2 carregada. Tentando spawnar a bola vermelha...");
            SpawnRedBall();

            // Iniciar o segundo timer
            secondSceneTimer = 0f; // Reseta o timer
            secondSceneTimerStarted = true; // Inicia o timer da cena 2
        }
    }

    private void SpawnRedBall()
    {
        if (redBallRef == null || redBall2Ref == null)
        {
            Debug.LogError("Referências para as bolas vermelhas não foram atribuídas!");
            return;
        }

        // Imprimir todos os GameObjects na cena para debug
        Debug.Log("GameObjects na cena:");
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            Debug.Log(obj.name);
        }

        // Verifica se já existe uma bola vermelha na cena
        if (GameObject.Find("RedBall") == null && GameObject.Find("RedBall2") == null)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0); // Defina uma posição no centro da tela

            // Escolhe aleatoriamente qual bola instanciar
            GameObject spawnedRedBall;
            if (Random.Range(0, 2) == 0)
            {
                spawnedRedBall = Instantiate(redBall2Ref, spawnPosition, Quaternion.identity);
                spawnedRedBall.name = "RedBall2"; // Nome para identificação
                Debug.Log($"Bola vermelha {spawnedRedBall.name} spawnada em {spawnPosition}");
            }
            else
            {
                spawnedRedBall = Instantiate(redBallRef, spawnPosition, Quaternion.identity);
                spawnedRedBall.name = "RedBall"; // Nome para identificação
                Debug.Log($"Bola vermelha {spawnedRedBall.name} spawnada em {spawnPosition}");
            }
        }
        else
        {
            Debug.Log("Uma bola vermelha já existe na cena.");
        }
    }

    public void StartGame()
    {
        startTimer = Time.time;
        timerStarted = true;
        sceneChanged = false;
        over = false; // Certificar-se de que o jogo não está no estado de Game Over
    }

    public void Score(GreenBall greenBall)
    {
        score++;
        scoreText.text = "Score: " + score; // Atualiza o texto do score
        StartCoroutine(greenBall.Respawn());
    }

    public void GameOver()
    {
        over = true;
        timerStarted = false; // Pausa o timer no Game Over
        restartButton.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void OnPlayerTouchRedBall()
    {
        GameOver();
    }

    public void Restart()
    {
        // Reiniciar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Reiniciar o timer e variáveis relacionadas
        ResetGame();
    }

    private void ResetGame()
    {
        // Reiniciar as variáveis importantes
        StartGame(); // Reinicia o timer e o controle de troca de cena
        score = 0; // Reinicia o score
        restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        // Certifique-se de que as bolas estão sendo respawnadas
        greenBall.Spawn(); // Spawn da GreenBall novamente

        Debug.Log("Tentando respawnar a bola vermelha no Restart...");
        SpawnRedBall(); // Spawn da RedBall novamente
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
