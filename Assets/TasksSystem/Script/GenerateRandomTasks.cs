using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomTasks : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] tasks;
    public int[] selectedTasks;

    private void Start()
    {
        SpawnRandomImage();
        Debug.Log("YES");

    }


    public void SpawnRandomImage()
    {
        // Check if there are prefabs available
        if (tasks.Length == 0)
        {
            Debug.LogError("No prefabs with Item script found.");
            return;
        }

        // Choose a random index
        int numberOfTasks = Random.Range(0, spawnPoints.Length);
        int randomTask = Random.Range(0, tasks.Length);
        int i = 0;

        while (i <= numberOfTasks)
        {
            Debug.Log(selectedTasks[i]);
            selectedTasks[i] = numberOfTasks;
            Instantiate(tasks[randomTask], spawnPoints[i].transform.position, Quaternion.identity);
            i++;
        }
    }
}
