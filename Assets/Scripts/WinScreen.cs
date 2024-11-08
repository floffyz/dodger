using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
  
    public TextMeshProUGUI score;


    void Start()
    {
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

        print(GameManager.Instance.score + " | " + PlayerPrefs.GetInt("highscore"));
// score.text = "SCORE: " + GameManager.Instance.score;
    }
}
