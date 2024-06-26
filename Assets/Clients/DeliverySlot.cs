using UnityEngine;
using UnityEngine.EventSystems;

public class DeliverySlot : MonoBehaviour, IDropHandler
{
    public GiveClientOrder giveClientOrder;

    private void Start()
    {
        giveClientOrder = GameObject.FindGameObjectWithTag("Player").GetComponent<GiveClientOrder>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null && giveClientOrder != null)
        {
            giveClientOrder.HandleDroppedItem(droppedItem);
        }
    }
}
