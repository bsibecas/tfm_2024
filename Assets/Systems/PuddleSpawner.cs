using UnityEngine;

public class PuddleSpawner : MonoBehaviour
{
    public GameObject puddlePrefab;
    public int maxPuddles = 5;
    public float spawnInterval = 5f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    private bool puddleEventTriggered = false;

    private void Start()
    {
        if (puddleEventTriggered)
            SpawnPuddles();
    }

    private void SpawnPuddles()
    {
        if (puddleEventTriggered && !PuddlesSpawned())
        {
            for (int i = 0; i < maxPuddles; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();
                Instantiate(puddlePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private bool PuddlesSpawned()
    {
        return GameObject.FindGameObjectsWithTag("Puddle").Length > 0;
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 7; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f), Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f), 0f);

            // Check if there are any colliders at the spawn position
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f); // Adjust the radius as needed
            bool isValidSpawnPosition = true;
            foreach (Collider2D collider in colliders)
            {
                if (!collider.isTrigger)
                {
                    isValidSpawnPosition = false;
                    break;
                }
            }

            // Check if the spawn position is too close to other puddles
            if (isValidSpawnPosition && !IsTooCloseToOtherPuddles(spawnPosition))
                return spawnPosition;
        }

        // If no valid position is found after 7 attempts, return the last position
        return transform.position;
    }


    private bool IsTooCloseToOtherPuddles(Vector3 position)
    {
        GameObject[] puddles = GameObject.FindGameObjectsWithTag("Puddle");
        foreach (GameObject puddle in puddles)
        {
            if (Vector3.Distance(puddle.transform.position, position) < 2f) // Adjust the distance as needed
                return true;
        }
        return false;
    }


    public void TriggerPuddleEvent()
    {
        if (!puddleEventTriggered)
        {
            puddleEventTriggered = true;
            SpawnPuddles();
        }
    }
}
