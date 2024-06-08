using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveClientOrder : MonoBehaviour
{
    public float interactionRange = 2.5f;
    public KeyCode interactionKey = KeyCode.E;
    public Inventory inventory;
    public Transform[] emptyPlaces;
    public GameObject check;
    public MoneyManager moneyManager;
    public TMP_Text deliverIndication;


    private void Awake()
    {
        AssignEmptyPlaces();
    }

    void AssignEmptyPlaces()
    {
        GameObject[] emptyPlaceObjects = GameObject.FindGameObjectsWithTag("EmptyPlaces");
        emptyPlaces = new Transform[emptyPlaceObjects.Length];
        for (int i = 0; i < emptyPlaceObjects.Length; i++)
        {
            emptyPlaces[i] = emptyPlaceObjects[emptyPlaceObjects.Length - 1 - i].transform;
        }
    }

    void Update()
    {
        CheckForClientInteraction();
    }

    private void CheckForClientInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        bool isClientNearby = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Client"))
            {
                isClientNearby = true;
                if (Input.GetKeyDown(interactionKey))
                {
                    GenerateRandomTasks clientTasks = collider.GetComponent<GenerateRandomTasks>();
                    if (clientTasks != null)
                    {
                        HandleClientInteraction(clientTasks);
                    }
                }
                if (deliverIndication != null)
                {
                    deliverIndication.text = "Click 'E' to deliver to the client your first inventory slot item";
                }
                break;
            } else
            {
                isClientNearby = false;
            }
        }

        if (!isClientNearby && deliverIndication != null)
        {
            deliverIndication.text = "";
        }
  
    }

    private void HandleClientInteraction(GenerateRandomTasks clientTasks)
    {
        bool correctTask = false;
        int findedTask = 0;
        GameObject[] orderedItems = clientTasks.GetOrderList();
        bool allTasksCompleted = false;

        if (inventory.isFull[0] == true)
        {
            Transform firstItemTransform = inventory.slots[0].transform.GetChild(0);
            GameObject firstItem = firstItemTransform.gameObject;
            for (int i = 0; i < orderedItems.Length; i++ )
            {
                if (orderedItems[i] != null)
                {
                    string orderedItemName = orderedItems[i].name.Replace("(Clone)", "").Trim();
                    string firstItemName = firstItem.name.Replace("(Clone)", "").Trim();

                    if (orderedItemName == firstItemName)
                    {
                        correctTask = true;
                        Transform emptySlot = emptyPlaces[i];
                        if (emptySlot != null)
                        {
                            Instantiate(check, emptySlot.position, Quaternion.identity, emptySlot.parent);
                        }
                        findedTask = i;
                        moneyManager.AddMoney(orderedItems[i].GetComponent<Item>().price);
                        orderedItems[i] = null;
                        break;
                    }
                }
            }
            if (correctTask == false)
            {
                GameManager.playerMoney = GameManager.playerMoney - orderedItems[findedTask].GetComponent<Item>().price;
                moneyManager.UpdateMoneyText();
            }

            allTasksCompleted = true;
            foreach (GameObject item in orderedItems)
            {
                if (item != null)
                {
                    allTasksCompleted = false;
                    break;
                }
            }

            if (allTasksCompleted)
            {
                GameManager.clients++;
            }

            Destroy(firstItem);
        }
    }
}
