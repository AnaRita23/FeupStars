using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuScript : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
