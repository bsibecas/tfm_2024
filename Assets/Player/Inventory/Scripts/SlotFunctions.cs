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
        if (transform.childCount <= 0 && !CompareTag("bin"))
        {
             inventory.isFull[i] = false;
        } else if (transform.childCount > 0)
        {
            inventory.isFull[i] = true;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    
    /*public void OnDrop(PointerEventData eventData)
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
    }*/

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = eventData.pointerDrag;
        GameObject dropTarget = eventData.pointerEnter;
    if (draggedItem != null && !CompareTag("bin"))
        {
            RectTransform itemRectTransform = draggedItem.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(slotRectTransform, Input.mousePosition, null))
            {
                if (!inventory.isFull[i])
                {
                    itemRectTransform.SetParent(slotRectTransform);
                    itemRectTransform.anchoredPosition = Vector2.zero;
                    inventory.isFull[i] = true;
                }
            }
        }
        else if (dropTarget.CompareTag("bin"))
        {
            Destroy(draggedItem);
        }
    } 
}
