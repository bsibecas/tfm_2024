using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotFunctions : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent <Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropTarget = eventData.pointerEnter;

        if (eventData.pointerDrag != null && dropTarget != null && dropTarget.CompareTag("bin"))
        {
            Destroy(eventData.pointerDrag);
        }
        else if (eventData.pointerDrag != null && inventory.isFull[i] != true)
        {
            RectTransform itemRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            itemRectTransform.SetParent(slotRectTransform);
            itemRectTransform.anchoredPosition = Vector2.zero;
            inventory.isFull[i] = true;
        }
    }
}
