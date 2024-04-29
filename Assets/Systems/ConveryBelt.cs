using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject[] items;
    public float spawnInterval = 1.5f;
    public Transform spawnPoint;
    public Transform destroyPoint;
    public float itemSpeed = 1.5f;
    public float destroyThreshold = 1f;

    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("BeltItem"))
        {
            DestroyItems(item);
        }
    }

    void ShuffleItems()
    {
        // Shuffle the items array using the Fisher-Yates algorithm
        for (int i = 0; i < items.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, items.Length);
            GameObject temp = items[randomIndex];
            items[randomIndex] = items[i];
            items[i] = temp;
        }
    }

    void SpawnItem()
    {
        if (currentIndex >= items.Length)
        {
            ShuffleItems();
            currentIndex = 0;
        }

        GameObject randomItem = items[currentIndex];
        GameObject spawnedItem = Instantiate(randomItem, spawnPoint.position, Quaternion.identity);
        spawnedItem.tag = "BeltItem";

        MoveItem(spawnedItem);

        currentIndex++;
    }



    void MoveItem(GameObject item)
    {
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }

        Vector2 direction = (destroyPoint.position - item.transform.position).normalized;
        rb.velocity = direction * itemSpeed;

       
    }

    void DestroyItems(GameObject item)
    {
        if (item.transform.position.y > destroyPoint.position.y)
        {
            Destroy(item);
        }
    }
}
