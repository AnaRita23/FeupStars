using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        if(elapsedTime <= 0.5)
        {
            timerText.text = "00:00";
            SceneManager.LoadScene("GameOver");
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
