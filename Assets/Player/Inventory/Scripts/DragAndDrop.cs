using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/*
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        // You can either set the canvas directly if you have multiple Canvases, or find it dynamically
        canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        // Find the DragCanvas and move this object to it
        Canvas dragCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
        rectTransform.SetParent(dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Move the object back to its original parent
        rectTransform.SetParent(originalParent, true);

        // Reset the position if the drop was invalid
        if (!eventData.pointerEnter || !eventData.pointerEnter.GetComponent<ItemSlot>() || eventData.pointerEnter.GetComponent<OnlyDragSlot>())
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

}
*/


public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        // You can either set the canvas directly if you have multiple Canvases, or find it dynamically
        canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        // Find the DragCanvas and move this object to it
        Canvas dragCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
        rectTransform.SetParent(dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //rectTransform.SetParent(originalParent, true);

        if (!eventData.pointerEnter || !eventData.pointerEnter.GetComponent<ItemSlot>() || eventData.pointerEnter.GetComponent<OnlyDragSlot>())
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
