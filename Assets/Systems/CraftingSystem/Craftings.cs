using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftings : MonoBehaviour
{
    [Header("---------------- UI ----------------")]
    public Image customCursor;

    [Header("---------------- Slots Info ----------------")]
    public GameObject[] craftingSlots;
    public bool[] isFull;
    public ItemSlot resultSlot;

    [Header("---------------- Items & Recipes ----------------")]
    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;

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
        string regularRecipeString = "";
        string reversedRecipeString = "";

        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
                regularRecipeString += item.itemName;
                reversedRecipeString = item.itemName + reversedRecipeString;
            }
            else
            {
                currentRecipeString += "null";
                regularRecipeString += "null";
                reversedRecipeString = "null" + reversedRecipeString;
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString || recipes[i] == reversedRecipeString)
            {
                DestroyOtherSlotsItems();
                Instantiate(recipeResults[i], resultSlot.transform, false);
                break;
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
