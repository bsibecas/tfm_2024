using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    public GameObject craftingInterface;
    public float pickupRange = 1.5f;
    public Transform playerTransform;
    public float distanceThreshold = 2.5f;

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
                    ShowCraftingInterface();
                }
            }
        }
    }

    void Update()
    {
        CheckMouseClick();
        float distanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer > distanceThreshold)
        {
            HideCraftingInterface();
        }
    }

    public void ShowCraftingInterface()
    {
        if (craftingInterface != null)
        {
            craftingInterface.SetActive(true);
        }
    }

    public void HideCraftingInterface()
    {
        if (craftingInterface != null)
        {
            craftingInterface.SetActive(false);
        }
    }
}
