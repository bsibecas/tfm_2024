using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item item;
    public int index;

    private Craftings craftingSlots;
    private CraftingsOven craftingSlotsOven;

    private void Start()
    {
        craftingSlots = GameObject.FindGameObjectWithTag("CraftingTable").GetComponent<Craftings>();
        craftingSlotsOven = GameObject.FindGameObjectWithTag("Oven").GetComponent<CraftingsOven>();

    }

    private void Update()
    {
        if (index >= 0 && index < craftingSlots.craftingSlots.Length)
        {
            if (transform.childCount <= 0)
            {
                craftingSlots.isFull[index] = false;
                craftingSlots.itemList[index] = null;
            }
        }
        if(index >= 0 && index < craftingSlotsOven.craftingSlotsOven.Length)
        {
            if (transform.childCount <= 0)
            {
                craftingSlotsOven.isFull[index] = false;
                craftingSlotsOven.itemList[index] = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null && craftingSlots.isFull[index] != true)
        {
            RectTransform itemRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            itemRectTransform.SetParent(slotRectTransform);
            itemRectTransform.anchoredPosition = Vector2.zero;
            craftingSlots.isFull[index] = true;
            Item droppedItem = eventData.pointerDrag.GetComponent<Item>();
            if (droppedItem != null)
            {
                item = droppedItem;
                craftingSlots.itemList[index] = item;
            }
        }
        if (eventData.pointerDrag != null && craftingSlotsOven.isFull[index] != true)
        {
            RectTransform itemRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();

            itemRectTransform.SetParent(slotRectTransform);
            itemRectTransform.anchoredPosition = Vector2.zero;
            craftingSlotsOven.isFull[index] = true;
            Item droppedItem = eventData.pointerDrag.GetComponent<Item>();
            if (droppedItem != null)
            {
                item = droppedItem;
                craftingSlotsOven.itemList[index] = item;
            }
        }
    }
}
