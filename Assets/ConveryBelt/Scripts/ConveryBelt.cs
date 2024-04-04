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

    void SpawnItem()
    {
        GameObject randomItem = items[Random.Range(0, items.Length)];
        GameObject spawnedItem = Instantiate(randomItem, spawnPoint.position, Quaternion.identity);
        spawnedItem.tag = "BeltItem";

        MoveItem(spawnedItem);
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
