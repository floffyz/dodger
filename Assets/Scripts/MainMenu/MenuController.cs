using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    private int highScore;

    public void Start()
    {
        highScoreText.text = (PlayerPrefs.GetInt("highscore")).ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
       // GameManager.Instance.StartGame();
    }


    public void Quit()
    {
        Application.Quit();
    }


}
