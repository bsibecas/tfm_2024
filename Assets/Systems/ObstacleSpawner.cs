using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int maxObstacle = 5;
    public float spawnInterval = 5f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    private bool obstacleEventTriggered = false;

    private void Start()
    {
        if (obstacleEventTriggered)
            SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        if (obstacleEventTriggered && !ObstaclesSpawned())
        {
            for (int i = 0; i < maxObstacle; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();
                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private bool ObstaclesSpawned()
    {
        if (obstaclePrefab.tag == "Puddle"){
            return GameObject.FindGameObjectsWithTag("Puddle").Length > 0;
        } else {
            return GameObject.FindGameObjectsWithTag("Trash").Length > 0;
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 7; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f), Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f), 0f);

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

            if (isValidSpawnPosition && !IsTooCloseToOtherObstacles(spawnPosition))
                return spawnPosition;
        }
        return transform.position;
    }


    private bool IsTooCloseToOtherObstacles(Vector3 position)
    {
        if (obstaclePrefab.tag == "Puddle"){
            GameObject[] puddles = GameObject.FindGameObjectsWithTag("Puddle");
            foreach (GameObject puddle in puddles)
            {
                if (Vector3.Distance(puddle.transform.position, position) < 2f)
                    return true;
            }
            return false;
        } else { 
            GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
            foreach (GameObject elem in trash)
            {
                if (Vector3.Distance(elem.transform.position, position) < 2f)
                    return true;
            }
            return false;
        }
    }

      public void TriggerObstacleEvent()
    {
        if (!obstacleEventTriggered)
        {
            obstacleEventTriggered = true;
            SpawnObstacles();
        }
    }
}
