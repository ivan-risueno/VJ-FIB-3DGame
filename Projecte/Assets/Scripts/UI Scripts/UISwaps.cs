using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwaps : MonoBehaviour
{
    public GameObject pauseMenu, gameOver;

    public void enablePauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void disablePauseMenu()
    {
        Invoke("disablePM", 0.5f);
    }

    public void enableGameOver()
    {
        gameOver.SetActive(true);
    }

    public void disableGameOver()
    {
        Invoke("disableGO", 0.5f);
    }

    void disablePM()
    {
        pauseMenu.SetActive(false);
    }

    void disableGO()
    {
        gameOver.SetActive(false);
    }
}

