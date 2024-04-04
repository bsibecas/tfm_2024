using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    private bool isVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleVisibility();
        }
    }

    void ToggleVisibility()
    {
        isVisible = !isVisible;

        foreach (GameObject slot in slots)
        {
            slot.SetActive(isVisible);
        }
    }
}
