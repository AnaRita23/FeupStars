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
}
