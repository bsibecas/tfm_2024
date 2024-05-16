using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] TimeSystem timeSystem;
    [SerializeField] GameObject clientPrefab;
    [SerializeField] float minDelayToSpawn;
    [SerializeField] float maxDelayToSpawn;

    private Transform selectedSpawnPoint;
    private void Start()
    {
        float rand = Random.Range(0f, 100f);

        if(rand <= 50f)
            selectedSpawnPoint = spawnPoints[0];
        else
            selectedSpawnPoint = spawnPoints[2];

        Instantiate(clientPrefab, selectedSpawnPoint.position, clientPrefab.transform.rotation);
    }
}
