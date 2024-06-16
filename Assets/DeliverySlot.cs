using UnityEngine;
using UnityEngine.EventSystems;

public class DeliverySlot : MonoBehaviour, IDropHandler
{
    public GiveClientOrder giveClientOrder;

    private void Start()
    {
        // Reference the GiveClientOrder script on the relevant GameObject (e.g., the player or another manager object)
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
