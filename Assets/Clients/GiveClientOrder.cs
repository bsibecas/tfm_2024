using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GiveClientOrder : MonoBehaviour
{
    public float interactionRange = 2.5f;
    public Inventory inventory;
    public Transform[] emptyPlaces;
    public MoneyManager moneyManager;
    public TMP_Text deliverIndication;
    public GameObject deliveryBag;

    AudioManager audioManager;

    public delegate void SatisfiedClient(float stressAmount);
    public static SatisfiedClient OnSatisfiedClient;

    public delegate void WrongDelivery(float stressAmount);
    public static WrongDelivery OnWrongDelivery;

    private void Awake()
    {
        AssignEmptyPlaces();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void AssignEmptyPlaces()
    {
        GameObject[] emptyPlaceObjects = GameObject.FindGameObjectsWithTag("EmptyCheck");
        emptyPlaces = new Transform[emptyPlaceObjects.Length];

        System.Array.Sort(emptyPlaceObjects, (a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
        for (int i = 0; i < emptyPlaceObjects.Length; i++)
        {
            emptyPlaces[i] = emptyPlaceObjects[i].transform;
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
                if (deliverIndication != null)
                {
                    deliverIndication.text = "Drop the item in the bag to deliver it to the client";
                }
                break;
            }
        }

        if (!isClientNearby && deliverIndication != null)
        {
            deliverIndication.text = "";
        }

        // Update the visibility of the deliveryBag based on the proximity to the client
        UpdateDeliveryBagVisibility(isClientNearby);
    }

    private void UpdateDeliveryBagVisibility(bool isClientNearby)
    {
        deliveryBag.SetActive(isClientNearby);
    }

    public void HandleDroppedItem(GameObject droppedItem)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        bool isClientNearby = false;
        GenerateRandomTasks clientTasks = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Client"))
            {
                isClientNearby = true;
                clientTasks = collider.GetComponent<GenerateRandomTasks>();
                break;
            }
        }

        if (isClientNearby && clientTasks != null)
        {
            HandleClientInteraction(clientTasks, droppedItem);
        }
    }

    private void HandleClientInteraction(GenerateRandomTasks clientTasks, GameObject firstItem)
    {
        bool correctTask = false;
        int findedTask = 0;
        GameObject[] orderedItems = clientTasks.GetOrderList();
        bool allTasksCompleted = false;

        string firstItemName = firstItem.name.Replace("(Clone)", "").Trim();

        for (int i = 0; i < orderedItems.Length; i++)
        {
            if (orderedItems[i] != null)
            {
                string orderedItemName = orderedItems[i].name.Replace("(Clone)", "").Trim();

                if (orderedItemName == firstItemName)
                {
                    correctTask = true;
                    OnSatisfiedClient(0.1f);
                    Transform emptySlot = emptyPlaces[i];
                    Image checkImage = emptySlot.GetComponentInChildren<Image>();
                    if (emptySlot != null)
                    {
                        Color color = checkImage.color;
                        color.a = 1f;
                        checkImage.color = color;
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
            GameManager.playerMoney = GameManager.playerMoney - Mathf.RoundToInt(firstItem.GetComponent<Item>().price / 2);
            moneyManager.UpdateMoneyText();
            OnWrongDelivery(0.2f);
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
            GameManager.satisfiedClients++;
            OnSatisfiedClient(0.1f);
        }

        Destroy(firstItem);
    }
}
