using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomTasks : MonoBehaviour
{
    public GameObject[] imagesToSpawn;
    public int maxNumberOfTasks = 3;
    public int numberOfTasks = 3;
    public GameObject[] orderList;

    public MoneyManager moneyManager;

    void Start()
    {
        GameManager.tasksCompleted = 0;
        numberOfTasks = Random.Range(1, numberOfTasks);

        orderList = new GameObject[numberOfTasks];

        for (int i = 0; i < numberOfTasks; i++)
        {
            GameObject randomImage = imagesToSpawn[Random.Range(0, imagesToSpawn.Length)];
            orderList[i] = randomImage;
        }

    }

    public GameObject[] GetOrderList()
    {
        return orderList;
    }

    public int GetTasksCompleted()
    {
        return GameManager.tasksCompleted;
    }

    public int GetNumberOfTasks()
    {
        return maxNumberOfTasks;
    }

}