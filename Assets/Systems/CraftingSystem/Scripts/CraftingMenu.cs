using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    public GameObject craftingCanvas;
    public float pickupRange = 1.5f;
    public Transform playerTransform;

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
                    ShowCraftingCanvas();
                }
            }
        }
    }

    void Update()
    {
        CheckMouseClick();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCraftingCanvas();
        }
    }

    public void ShowCraftingCanvas()
    {
        if (craftingCanvas != null)
        {
            craftingCanvas.SetActive(true);
        }
    }

    public void HideCraftingCanvas()
    {
        if (craftingCanvas != null)
        {
            craftingCanvas.SetActive(false);
        }
    }
}
