using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // For Button

public class MaterialsShop : MonoBehaviour
{
    public GameObject[] materials;
    public TMP_Text[] prices;
    public MoneyManager moneyManager;
    public Inventory inventory;

    private void Start()
    {
        RemoveDragAndDropComponents();
        UpdatePrices();
        AddClickListeners();
    }

    void RemoveDragAndDropComponents()
    {
        foreach (GameObject material in materials)
        {
            DragAndDrop dragAndDrop = material.GetComponent<DragAndDrop>();
            if (dragAndDrop != null)
            {
                Destroy(dragAndDrop);
            }
        }
    }

    private void UpdatePrices()
    {
        for (int i = 0; i < prices.Length; i++)
        {
            if (i < materials.Length)
            {
                Item item = materials[i].GetComponent<Item>();
                if (item != null)
                {
                    prices[i].text = item.price.ToString() + "$";
                }
            }
        }
    }

    private void AddClickListeners()
    {
        foreach (GameObject material in materials)
        {
            Button button = material.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnItemClicked(material));
            }
        }
    }

    public bool CanBuyItem(Item item)
    {
        return GameManager.playerMoney >= item.price;
    }

    public void OnItemClicked(GameObject materialPrefab)
    {
        Item item = materialPrefab.GetComponent<Item>();

        if (CanBuyItem(item))
        {
            int slotIndex = inventory.FindFirstAvailableSlot();

            if (slotIndex != -1)
            {
                GameObject newItem = Instantiate(materialPrefab, inventory.slots[slotIndex].transform.position, Quaternion.identity);
                newItem.AddComponent<DragAndDrop>();
                newItem.transform.SetParent(inventory.slots[slotIndex].transform, false);
                newItem.transform.localPosition = Vector3.zero;
                inventory.isFull[slotIndex] = true;
                moneyManager.SubstractPlayerMoney(item.price);
            }
            else
            {
                Debug.Log("Inventory is full. Cannot purchase " + item.itemName);
            }
        }
        else
        {
            Debug.Log("Not enough money to buy " + item.itemName);
        }
    }

}
