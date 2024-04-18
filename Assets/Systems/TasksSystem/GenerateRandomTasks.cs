using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerateRandomTasks : MonoBehaviour
{
    public GameObject canvas;
    public GameObject[] imagesToSpawn;
    public int numberOfImagesToSpawn = 3;
    public ItemSlot orderSlot;
    public GameObject[] orderList;
    float spacingY = 160f;
    public float destroyDelay = 2f;
    public GameObject check;
    public int tasksCompleted = 0;

    void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned in ImageSpawner.");
            return;
        }

        Vector3 initialPosition = new Vector3(-145f, 190f, 0f);
        orderList = new GameObject[numberOfImagesToSpawn];

        for (int i = 0; i < numberOfImagesToSpawn; i++)
        {
            GameObject randomImage = Instantiate(imagesToSpawn[Random.Range(0, imagesToSpawn.Length - 1)], Vector3.zero, Quaternion.identity, canvas.transform);
            Destroy(randomImage.GetComponent<DragAndDrop>());

            randomImage.transform.localPosition = initialPosition - new Vector3(0f, i * spacingY + 5, 0f);

            RectTransform rectTransform = randomImage.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
            orderList[i] = randomImage;
        }
    }


    private void Update()
    {
        CheckOrderSlot();
    }

    private IEnumerator DestroyItemWithDelay(GameObject item)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(item);
    }


    private void CheckOrderSlot()
    {
        if (orderSlot.item != null)
        {
            for (int i = 0; i < orderList.Length; i++)
            {
                if (orderList[i] != null)
                {
                    string orderItemName = orderSlot.item.GetComponent<Item>().itemName;
                    string listItemName = orderList[i].GetComponent<Item>().itemName;

                    if (orderItemName == listItemName)
                    {
                        Vector3 position = new Vector3(700f, i * - 135 + i * (i * i) * 5 + 485, 0f);
                        GameObject instantiatedImage = Instantiate(check, position, Quaternion.identity, canvas.transform);
                        RectTransform rectTransform = instantiatedImage.GetComponent<RectTransform>();
                        rectTransform.localScale = new Vector3(1f, 1f, 1f);
                        Destroy(orderSlot.item.gameObject);
                        orderList[i] = null;
                        tasksCompleted++;
                        break;
                    }
                }
            }
        }
    }
    public int GetTasksCompleted()
    {
        return tasksCompleted;
    }

    public int GetNumberOfTasks()
    {
        return numberOfImagesToSpawn;
    }

}
