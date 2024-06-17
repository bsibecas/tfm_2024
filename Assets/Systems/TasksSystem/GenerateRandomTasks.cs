using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomTasks : MonoBehaviour
{
    public GameObject[] imagesToSpawn;
    public int numberOfTasks = 3;
    public int maxTasks = 3;
    public int newRecipe = 0;
    public GameObject[] orderList;

    public MoneyManager moneyManager;

    void Start()
    {
        if (GameManager.days <= 3)
        {
            newRecipe = GameManager.days;
        } else
        {
            newRecipe = 3;
        }
        numberOfTasks = Random.Range(1, maxTasks);

        orderList = new GameObject[numberOfTasks];

        for (int i = 0; i < numberOfTasks; i++)
        {
            GameObject randomImage = imagesToSpawn[Random.Range(0, (maxTasks + newRecipe))];

            orderList[i] = randomImage;
        }

    }

    public GameObject[] GetOrderList()
    {
        return orderList;
    }

}