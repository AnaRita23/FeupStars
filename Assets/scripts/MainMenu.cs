using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayerVsComputer()
    {
        SceneManager.LoadScene("Player Vs Computer");
    }

    public void PlayerVsPlayer()
    {
        SceneManager.LoadScene("Player Vs Player");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

