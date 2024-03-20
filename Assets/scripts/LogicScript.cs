using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour

{
    public int playerScore = 0;
    public int enemyScore = 0;
    public Text playerText;
    public Text enemyText;

    void Start()
    {
        PlayerPrefs.SetInt("Player1Score", playerScore);
        PlayerPrefs.SetInt("Player2Score", enemyScore);
    }

    [ContextMenu("Increase P Score")]
    public void addPlayerScore()
    {
        playerScore += 1;
        playerText.text = playerScore.ToString();
        PlayerPrefs.SetInt("Player1Score", playerScore);
    }

    [ContextMenu("Increase E Score")]
    public void addEnemyScore()
    {
        enemyScore += 1;
        enemyText.text = enemyScore.ToString();
        PlayerPrefs.SetInt("Player2Score", enemyScore);

    }

    public void addPowerUpPlayerScore()
    {
        playerScore += 2;
        playerText.text = playerScore.ToString();
        PlayerPrefs.SetInt("Player1Score", playerScore);
    }

    [ContextMenu("Increase E Score")]
    public void addPowerUpEnemyScore()
    {
        enemyScore += 2;
        enemyText.text = enemyScore.ToString();
        PlayerPrefs.SetInt("Player2Score", enemyScore);
    }

    public void loosePlayerPoint()
    {
        if (playerScore > 0)
        {
            playerScore -= 1;
        }
        playerText.text = playerScore.ToString();
        PlayerPrefs.SetInt("Player1Score", playerScore);
    }

    public void looseEnemyPoint()
    {
        if (enemyScore > 0)
        {
            enemyScore -= 1;
        }
        enemyText.text = enemyScore.ToString();
        PlayerPrefs.SetInt("Player2Score", enemyScore);
    }
}