using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


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
