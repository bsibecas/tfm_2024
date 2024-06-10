using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTasksBoard : MonoBehaviour
{
    public GameObject tasksCanvas;
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
                     ShowTasksCanvas();
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
            HideTasksCanvas();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideTasksCanvas();
        }
    }

    public void ShowTasksCanvas()
    {
        if (tasksCanvas != null)
        {
            tasksCanvas.SetActive(true);
        }
    }

    public void HideTasksCanvas()
    {
        if (tasksCanvas != null)
        {
            tasksCanvas.SetActive(false);
        }
    }
}
