using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
  
    public TextMeshProUGUI score;


    void Start()
    {

        GameManager.Instance.scoreText.enabled = false;
        GameManager.Instance.timeLeftText.enabled = false;

        if (GameManager.Instance.score > PlayerPrefs.GetInt("highscore"))
        {

            PlayerPrefs.SetInt("highscore", GameManager.Instance.score);
            PlayerPrefs.Save();
            score.text = "NEW HIGHSCORE: " + GameManager.Instance.score;

        }
        else
        {

            score.text = "SCORE: " + GameManager.Instance.score;

        }

    }


    public void Restart()
    {

        SceneManager.LoadScene("MainScene");
        GameManager.Instance.ResetGame();

    }

}
