using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalPrefab;
    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);
    public float delay = 2f;

    private void Start()
    {
        // Start coroutine to activate portals after delay
        StartCoroutine(ActivatePortals());
    }

    IEnumerator ActivatePortals()
    {
        // Wait for delay before activating portals
        yield return new WaitForSeconds(delay);


        Vector2 portal1Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
        Vector2 portal2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));

        
        while (Vector2.Distance(portal1Position, portal2Position) < 4f)
        {
            portal2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
            
        }

        // Instantiate portals
        GameObject portal1 = Instantiate(portalPrefab, portal1Position, Quaternion.identity);
        GameObject portal2 = Instantiate(portalPrefab, portal2Position, Quaternion.identity);

    }
}

