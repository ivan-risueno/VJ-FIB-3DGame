using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public static int highScore;

    void Start()
    {
        if (highScore < PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    void Update()
    {
        highScoreText.text = highScore.ToString();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

}
