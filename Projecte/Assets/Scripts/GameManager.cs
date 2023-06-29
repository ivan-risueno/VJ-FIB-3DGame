using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator gameOver;
    int coins = 0;
    int score = 0;
    public static bool paused = false;

    public void die()
    {
        gameOver.SetTrigger("Show");
        pauseGame();

    }

    public void playLevel()
    {

    }

    public void sumCoin()
    {

    }

    public void sumScore()
    {

    }

    public void win()
    {
        pauseGame();
    }

    public void pauseGame()
    {
        paused = true;
        Invoke("pause", 0.5f); 
    }

    public void resumeGame()
    {
        paused = false;
        Time.timeScale = 1f;
    }


    public void loadMainMenu()
    {
        Invoke("loadMenu", 0.5f);
    }

    void loadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void retryLevel()
    {
        Invoke("retry", 0.5f);
    }

    void retry()
    {
        SceneManager.LoadScene("LevelScene");
    }

    void pause()
    {
        Time.timeScale = 0f;
    }
}
