using UnityEngine;

public class OpenDeliverTasks : MonoBehaviour
{
    public GameObject deliverCanvas;
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
                    ShowDeliverCanvas();
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
            HideDeliverCanvas();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideDeliverCanvas();
        }
    }

    public void ShowDeliverCanvas()
    {
        if (deliverCanvas != null)
        {
            deliverCanvas.SetActive(true);
        }
    }

    public void HideDeliverCanvas()
    {
        if (deliverCanvas != null)
        {
            deliverCanvas.SetActive(false);
        }
    }
}
