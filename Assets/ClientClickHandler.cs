using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClientClickHandler : MonoBehaviour, IDropHandler
{
    public GenerateRandomTasks clientTasks;
    public Transform[] emptyPlaces;
    public float pickupRange = 2.5f;
    public Transform playerTransform;
    public TMP_Text[] taskPrice;
    public Animator animator;

    private int actualTask = 0;

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
            for (actualTask = 0; orderedItems != null && actualTask < orderedItems.Length; actualTask++)
            {
                GameObject orderedItem = orderedItems[actualTask];
                if (orderedItem != null)
                {
                    ReplaceEmptySlotWithItem(orderedItem, actualTask);
                }
            }
            animator.SetBool("isOpen", true);
        }
    }

    private void ReplaceEmptySlotWithItem(GameObject orderedItem, int index)
    {
        if (index < emptyPlaces.Length)
        {
            Transform emptySlot = emptyPlaces[index];
            if (emptySlot != null)
            {
                GameObject newItem = Instantiate(orderedItem, emptySlot.position, Quaternion.identity, emptySlot.parent);
                newItem.transform.SetParent(emptySlot.parent, false);

                Item itemComponent = newItem.GetComponent<Item>();
                if (itemComponent != null)
                {
                    int itemPrice = itemComponent.price;
                    taskPrice[index].text = itemPrice.ToString() + "$";
                }
            }
        }
    }

    // Implement the IDropHandler interface method
    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item matches the current task
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Item>() != null)
        {
            Item droppedItem = eventData.pointerDrag.GetComponent<Item>();
            GameObject[] orderedItems = clientTasks.GetOrderList();

            if (actualTask < orderedItems.Length && orderedItems[actualTask] == eventData.pointerDrag.gameObject)
            {
                // Mark the task as done
                Debug.Log("Task " + actualTask + " is marked as done.");
                actualTask++;

                // Play the client's animation
                animator.SetBool("isOpen", true);

                // Update the UI to reflect the completed task
                UpdateTaskUI();
            }
        }
    }

    private void UpdateTaskUI()
    {
        if (actualTask < emptyPlaces.Length && actualTask < taskPrice.Length)
        {
            // Update the task UI to indicate the completed task
            Debug.Log("Updating task UI for task " + actualTask);
            Destroy(emptyPlaces[actualTask].gameObject); // Remove the task placeholder
            taskPrice[actualTask].text = "Done"; // Update task price text
        }
        else
        {
            Debug.LogWarning("No more tasks or UI elements to update.");
        }
    }
}
