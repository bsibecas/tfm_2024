using System.Collections;
using UnityEngine;

public class ActivateInLevel : MonoBehaviour
{
    public GameObject Recipe;
    public GameObject Recipe1;
    public GameObject Recipe2;


    private void Awake()
    {
        Recipe.SetActive(false);
        Recipe.SetActive(false);
        Recipe.SetActive(false);

    }

    private void Update()
    {
        if (GameManager.days == 1)
        {
            Recipe.SetActive(true);
        }
        else if (GameManager.days == 2)
        {
            Recipe.SetActive(true);
        }
        else if (GameManager.days == 3)
        {
            Recipe.SetActive(true);
        }

    }
}
