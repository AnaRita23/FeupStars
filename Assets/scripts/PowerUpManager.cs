using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);
    public float delay = 2f;

    private void Start()
    {
        // Start coroutine to activate portals after delay
        StartCoroutine(ActivatePowerUps());
    }

    IEnumerator ActivatePowerUps()
    {
        // Wait for delay before activating portals
        yield return new WaitForSeconds(delay);


        Vector2 powerUp1Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
        Vector2 powerUp2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));


        while (Vector2.Distance(powerUp1Position, powerUp2Position) < 4f)
        {
            powerUp2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));

        }

        // Instantiate power-ups
        GameObject powerUp1 = Instantiate(powerUpPrefab, powerUp1Position, Quaternion.identity);
        GameObject powerUp2 = Instantiate(powerUpPrefab, powerUp2Position, Quaternion.identity);

    }
}

