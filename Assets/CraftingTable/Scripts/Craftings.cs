using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftings : MonoBehaviour
{

    public Image customCursor;

    public GameObject[] craftingSlots;
    public bool[] isFull;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public ItemSlot resultSlot;

    private void Update()
    {
        CheckForCreatedRecipes();
    }

    public void OnClickslot(ItemSlot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    void CheckForCreatedRecipes()
    {
        resultSlot.item = null;
        string currentRecipeString = "";

        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                DestroyOtherSlotsItems();
                Instantiate(recipeResults[i], resultSlot.transform, false);
            }
        }
    }

    void DestroyOtherSlotsItems()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (craftingSlots[i] != resultSlot.gameObject)
            {
                ItemSlot slot = craftingSlots[i].GetComponent<ItemSlot>();
                if (slot != null && slot.item != null)
                {
                    Destroy(slot.item.gameObject);
                }
            }
        }
    }
}
