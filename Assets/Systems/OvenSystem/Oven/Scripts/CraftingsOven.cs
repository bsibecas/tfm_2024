using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingsOven : MonoBehaviour
{
    public float castingTime;
    public float MaximumCastingTime = 3f;
    public bool activatedTime = false;

    public Image customCursor;

    public GameObject[] craftingSlotsOven;

    public bool[] isFull;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public ItemSlot resultSlot;
    public Slider slider;

    private int recipeNumber;

    private void Update()
    {
        CheckForCreatedRecipes();

        if (activatedTime)
        {
            CheckTime();
        }
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
        instantiateItems(currentRecipeString, reversedRecipeString);

    }

    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyOtherSlotsItems();
    }

    void instantiateItems(string currentRecipeString, string reversedRecipeString)
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString || recipes[i] == reversedRecipeString)
            {
                ActiveTime();
                recipeNumber = i;
                StartCoroutine(DelayedDestroy(0.3f));

                break;
            }
        }
    }


    void DestroyOtherSlotsItems()
    {
        for (int i = 0; i < craftingSlotsOven.Length; i++)
        {
            if (craftingSlotsOven[i] != resultSlot.gameObject)
            {
                ItemSlot slot = craftingSlotsOven[i].GetComponent<ItemSlot>();
                if (slot != null && slot.item != null)
                {
                    Destroy(slot.item.gameObject);
                }
            }
        }

    }

    private void CheckTime()
    {
        castingTime -= Time.deltaTime;

        if (castingTime >= 0)
        {
            slider.value = castingTime;
        }
        if (castingTime <= 0)
        {
            StatusTimeChange(false);
            Debug.Log("Fundido");
            Instantiate(recipeResults[recipeNumber], resultSlot.transform, false);

        }
    }

    private void StatusTimeChange(bool status)
    {
        activatedTime = status;
    }

    public void ActiveTime()
    {
        castingTime = MaximumCastingTime;
        slider.maxValue = MaximumCastingTime;
        StatusTimeChange(true);
    }
}