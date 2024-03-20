using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text playerScoreText;
    public Text enemyScoreText;


    // Start is called before the first frame update
    void Start()
    {
        int playerScore = PlayerPrefs.GetInt("Player1Score", 0);
        int enemyScore = PlayerPrefs.GetInt("Player2Score", 0);

        // Update UI text to display scores
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();

    }


}
