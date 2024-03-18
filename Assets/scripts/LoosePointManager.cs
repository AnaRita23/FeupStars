using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoosePointManager : MonoBehaviour
{
    public GameObject loosePointPrefab;
    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);
    public float delay = 2f;

    private bool canSpawnPortals = true;
    private GameObject[] portals;

    private void Start()
    {
        portals = new GameObject[2]; // Inicializa o array para armazenar os portais
        // Start coroutine to activate portals after delay
        StartCoroutine(ActivatePortals());
    }

    IEnumerator ActivatePortals()
    {
        // Wait for delay before activating portals
        yield return new WaitForSeconds(delay);

        while(true)
        {
            if (canSpawnPortals && portals[0] == null && portals[1] == null)
            {
                // to prevent immediate respawning
                canSpawnPortals = false;

                Vector2 portal1Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
                Vector2 portal2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));


                while (Vector2.Distance(portal1Position, portal2Position) < 4f)
                {
                    portal2Position = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));

                }

                // Instantiate portals
                portals[0] = Instantiate(loosePointPrefab, portal1Position, Quaternion.identity);
                portals[1] = Instantiate(loosePointPrefab, portal2Position, Quaternion.identity);

                // Add colliders to the portals
                portals[0].AddComponent<BoxCollider2D>();
                portals[1].AddComponent<BoxCollider2D>();

                StartCoroutine(ResetPortalSpawn());

            }

            // Delay before checking if portals can be spawned again
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ResetPortalSpawn()
    {
        // Wait for 10 seconds before allowing portals to be spawned again
        yield return new WaitForSeconds(20f);

        // Set canSpawnPortals back to true to allow portal spawning
        canSpawnPortals = true;
    }
}