using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{

    public void playGame()
    {
        Invoke("play", 0.5f);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void play()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
