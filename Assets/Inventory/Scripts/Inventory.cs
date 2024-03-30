using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    private bool isVisible = true;

    void Update()
    {
        // Toggle inventory visibility when the "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleVisibility();
        }
    }

    void ToggleVisibility()
    {
        isVisible = !isVisible;

        // Set the visibility of each slot
        foreach (GameObject slot in slots)
        {
            slot.SetActive(isVisible);
        }
    }
}
