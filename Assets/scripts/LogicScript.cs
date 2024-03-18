using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour

{
    public int playerScore = 0;
    public int enemyScore = 0;
    public Text scoreText;

    [ContextMenu("Increase P Score")]
    public void addPlayerScore()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }

    [ContextMenu("Increase E Score")]
    public void addEnemyScore()
    {
        enemyScore += 1;
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }

    public void addPowerUpPlayerScore()
    {
        playerScore += 2;
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }

    [ContextMenu("Increase E Score")]
    public void addPowerUpEnemyScore()
    {
        enemyScore += 2;
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }

    public void loosePlayerPoint()
    {
        if (playerScore > 0)
        {
            playerScore -= 1;
        }
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }

    public void looseEnemyPoint()
    {
        if (enemyScore > 0)
        {
            enemyScore -= 1;
        }
        scoreText.text = playerScore.ToString() + " - " + enemyScore.ToString();
    }
}
