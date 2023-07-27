using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public Text score;

    void Start()
    {
        score.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExBtn()
    {
        Application.Quit();
    }
}
