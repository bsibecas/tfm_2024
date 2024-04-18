using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRecipeBook : MonoBehaviour
{
    public GameObject recipeBookCanvas;
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isOpen)
        {
            ShowRecipeCanvas();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && isOpen)
        {
            HideRecipeCanvas();
        }
    }

    public void ShowRecipeCanvas()
    {
        if (recipeBookCanvas != null)
        {
            recipeBookCanvas.SetActive(true);
            isOpen = true;
        }
    }

    public void HideRecipeCanvas()
    {
        if (recipeBookCanvas != null)
        {
            recipeBookCanvas.SetActive(false);
            isOpen = false;
        }
    }
}
