using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class AI_Manager : MonoBehaviour
{
    SerializeField] Transform[] spawnPoints;
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
*/

/*

public class ClientManager : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private int maxClients = 1;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform spawnPoints;

    public List<AI_Client> activeClients = new List<AI_Client>();

    private void Start()
    {
        StartCoroutine(SpawnClients());
    }

    private IEnumerator SpawnClients()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (activeClients.Count < maxClients)
            {
                SpawnClient();
            }
        }
    }

    private void SpawnClient()
    {
        GameObject newClient = Instantiate(clientPrefab, spawnPoints.position, Quaternion.identity);
        AI_Client clientScript = newClient.GetComponent<AI_Client>();
    //    clientScript.OnClientExit += HandleClientExit;
        activeClients.Add(clientScript);
    }

    private void HandleClientExit(AI_Client client)
    {
        activeClients.Remove(client);
    }
}*/

public class ClientManager : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform spawnPoints;

    private AI_Client activeClient;

    private void Start()
    {
        StartCoroutine(CheckAndSpawnClient());
    }

    private IEnumerator CheckAndSpawnClient()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (activeClient == null)
            {
                SpawnClient();
            }
        }
    }

    private void SpawnClient()
    {
        if (clientPrefab != null && spawnPoints != null)
        {
            GameObject newClient = Instantiate(clientPrefab, spawnPoints.position, Quaternion.identity);
            AI_Client clientScript = newClient.GetComponent<AI_Client>();
            clientScript.OnClientExit += HandleClientExit;
            activeClient = clientScript;
            GameManager.clients++;
        }
        else
        {
            Debug.LogWarning("Client prefab or spawn points are not assigned.");
        }
    }

    private void HandleClientExit(AI_Client client)
    {
        if (client != null)
        {
            client.OnClientExit -= HandleClientExit;
            if (client == activeClient)
            {
                activeClient = null;
            }
        }
    }
}

