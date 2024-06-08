using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClientClickHandler : MonoBehaviour
{
    public GenerateRandomTasks clientTasks;
    public Transform[] emptyPlaces;
    public float pickupRange = 2.5f;
    public Transform playerTransform;
    public TMP_Text[] taskPrice;
    public Animator animator;

    private int actualTask = 0;
    private GameObject[] instantiatedItems;

    void Awake()
    {
        AssignPlayerTransform();
        AssignEmptyPlaces();
        AssignAnimator();
        AssignTaskPriceTexts();
    }

    void AssignPlayerTransform()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found. Ensure it is tagged 'Player'.");
        }
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

    void AssignAnimator()
    {
        GameObject animatorObject = GameObject.FindWithTag("TaskAnimator");
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator component not found on the GameObject with tag 'ClientAnimator'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'ClientAnimator' not found.");
        }
    }

    void AssignTaskPriceTexts()
    {
        GameObject taskPriceParent = GameObject.FindWithTag("TaskPriceParent");
        if (taskPriceParent != null)
        {
            taskPrice = taskPriceParent.GetComponentsInChildren<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("Task price parent GameObject not found. Ensure it is tagged 'TaskPriceParent'.");
        }
    }


    void Update()
    {
        CheckMouseClick();
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                float distanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);

                if (distanceToPlayer <= pickupRange)
                {
                    HandleClientClick();
                }
            }
        }
    }

    private void HandleClientClick()
    {
        if (clientTasks != null)
        {
            GameObject[] orderedItems = clientTasks.GetOrderList();
            instantiatedItems = new GameObject[orderedItems.Length];

            for (actualTask = 0; orderedItems != null && actualTask < orderedItems.Length; actualTask++)
            {
                GameObject orderedItem = orderedItems[actualTask];
                if (orderedItem != null)
                {
                    instantiatedItems[actualTask] = ReplaceEmptySlotWithItem(orderedItem, actualTask);
                }
            }
            animator.SetBool("isOpen", true);
        }
    }

    private GameObject ReplaceEmptySlotWithItem(GameObject orderedItem, int index)
    {
        GameObject newItem = null;

        if (index < emptyPlaces.Length)
        {
            Transform emptySlot = emptyPlaces[index];
            if (emptySlot != null)
            {
                newItem = Instantiate(orderedItem, emptySlot.position, Quaternion.identity, emptySlot.parent);

                Item itemComponent = newItem.GetComponent<Item>();
                if (itemComponent != null)
                {
                    int itemPrice = itemComponent.price;
                    taskPrice[index].text = itemPrice.ToString() + "$";
                }
            }
        }
        return newItem;
    }

    void OnDestroy()
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", false);
        }
        if (instantiatedItems != null)
        {
            foreach (GameObject item in instantiatedItems)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
        }
        for (int i = 0; i < taskPrice.Length; i++)
        {
            taskPrice[i].text = "";
        }
    }

}
