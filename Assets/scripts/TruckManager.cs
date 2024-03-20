using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public GameObject loosePointPrefab;
    public GameObject enemy;
    public GameObject player;
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
        if (canSpawnPortals && portals[0] == null)
        {
            Vector2 portalPosition = GetValidPortalPosition();

            // Instantiate portal
            portals[0] = Instantiate(loosePointPrefab, portalPosition, Quaternion.identity);

            // Add collider to the portal
            portals[0].AddComponent<BoxCollider2D>();

            // Set layer collision to ignore collisions between ball and power-ups
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("PowerUps"));

            // Prevent immediate respawning
            canSpawnPortals = false;

            StartCoroutine(ResetPortalSpawn());
        }

        // Delay before checking if portal can be spawned again
        yield return new WaitForSeconds(0.5f);
    }
}

Vector2 GetValidPortalPosition()
{
    Vector2 portalPosition;
    do
    {
        portalPosition = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
    }
    while (Vector2.Distance(portalPosition, player.transform.position) < 2f || Vector2.Distance(portalPosition, enemy.transform.position) < 2f);
    
    return portalPosition;
}
    IEnumerator ResetPortalSpawn()
    {
        // Wait for 10 seconds before allowing portals to be spawned again
        yield return new WaitForSeconds(20f);

        // Set canSpawnPortals back to true to allow portal spawning
        canSpawnPortals = true;
    }
}