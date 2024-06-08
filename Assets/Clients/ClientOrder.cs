using System.Collections.Generic;
using UnityEngine;

public class ClientOrder : MonoBehaviour
{
    public List<GameObject> orderItems; // List of items the client has ordered

    public void AddOrderItem(GameObject item)
    {
        orderItems.Add(item);
    }

    public void RemoveOrderItem(GameObject item)
    {
        orderItems.Remove(item);
    }

    public bool HasOrderedItem(GameObject item)
    {
        return orderItems.Contains(item);
    }
}
