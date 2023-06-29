using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwap : MonoBehaviour
{
    public GameObject MainMenu, Instructions, Credits;

    public void disableMainMenu()
    {
        Invoke("disableMM", 0.5f);
    }

    public void enableMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void disableInstructions()
    {
        Invoke("disableIns", 0.5f);
    }

    public void enableInstructions()
    {
        Instructions.SetActive(true);
    }

    public void disableCredits()
    {
        Invoke("disableCts", 0.5f);
    }

    public void enableCredits()
    {
        Credits.SetActive(true);
    }

    void disableMM()
    {
        MainMenu.SetActive(false);
    }

    void disableIns()
    {
        Instructions.SetActive(false);
    }

    void disableCts()
    {
        Credits.SetActive(false);
    }
}
