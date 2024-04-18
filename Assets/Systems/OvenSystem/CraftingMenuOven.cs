using UnityEngine;

public class CraftingMenuOven : MonoBehaviour
{
    public GameObject OvenCanvas;
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
                    ShowOvenCanvas();
                }
            }
        }
    }

    void Update()
    {
        CheckMouseClick();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideOvenCanvas();
        }
    }

    public void ShowOvenCanvas()
    {
        if (OvenCanvas != null)
        {
            OvenCanvas.SetActive(true);
        }
    }

    public void HideOvenCanvas()
    {
        if (OvenCanvas != null)
        {
            OvenCanvas.SetActive(false);
        }
    }
}
