using UnityEngine;

public class HidePlayerCanvas : MonoBehaviour
{
    public Canvas[] canvasesToDeactivate;

    void Start()
    {
        gameObject.SetActive(true);
        DeactivateOtherCanvases();
    }

    void OnEnable()
    {
        DeactivateOtherCanvases();
    }

    void DeactivateOtherCanvases()
    {
        foreach (Canvas canvas in canvasesToDeactivate)
        {
            if (canvas != null && canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
