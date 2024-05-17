using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject timeSystemContainer;
    [SerializeField] GameObject clientPrefab;
    [SerializeField] float minDelayToSpawn;
    [SerializeField] float maxDelayToSpawn;

    private Transform selectedSpawnPoint;
    private TimeSystem timeSystem;
    private bool firstClientSpawned = false;
    private bool isWaitingNextSpawn = false;
    private float spawnDelay = 0f;

    private void Awake()
    {
        timeSystem = timeSystemContainer.GetComponent<TimeSystem>();
    }

    private void Update()
    {
        if(timeSystem.GetCastingTime() <= 270f && !firstClientSpawned) // castingTime at 4,5 min = 270f -> 60 * 4,5 = 270
        {
            SpawnClient();
            firstClientSpawned = true;
            isWaitingNextSpawn = true;
        }

        if(isWaitingNextSpawn)
        {
            StartCoroutine(WaitAndSpawn());
        }
    }

    private void SpawnClient()
    {
        SelectSpawnPoint();
        Instantiate(clientPrefab, selectedSpawnPoint.position, clientPrefab.transform.rotation);
        isWaitingNextSpawn = true;
    }

    private void SelectSpawnPoint()
    {
        float rand = Random.Range(0f, 100f);

        if (rand <= 50f)
            selectedSpawnPoint = spawnPoints[0];
        else
            selectedSpawnPoint = spawnPoints[2];
    }

    private float GetRandDelay()
    {
        float rand = Random.Range(minDelayToSpawn, maxDelayToSpawn);
        return rand;
    }

    IEnumerator WaitAndSpawn()
    {
        isWaitingNextSpawn = false;
        spawnDelay = GetRandDelay();
        yield return new WaitForSeconds(spawnDelay);
        SpawnClient();
    }
}
