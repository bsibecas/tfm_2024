using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomTasks : MonoBehaviour
{
    public GameObject canvas;
    public GameObject[] imagesToSpawn;
    public int numberOfImagesToSpawn = 3;

    private List<GameObject> spawnedImages = new List<GameObject>();

    void Start()
    {
        // Check if the canvas is assigned
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned in ImageSpawner.");
            return;
        }

        float spacingY = 120f;
        Vector3 initialPosition = new Vector3(-145f, 190f, 0f);

        for (int i = 0; i < numberOfImagesToSpawn; i++)
        {
            GameObject randomImage = Instantiate(imagesToSpawn[Random.Range(0, imagesToSpawn.Length -1)], Vector3.zero, Quaternion.identity, canvas.transform);
            Destroy(randomImage.GetComponent<DragAndDrop>());

            randomImage.transform.localPosition = initialPosition - new Vector3(0f, i * spacingY, 0f);

            RectTransform rectTransform = randomImage.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
            spawnedImages.Add(randomImage);
        }
    }

    private void OnImageDrop(GameObject image)
    {
        if (spawnedImages.Contains(image))
        {
            spawnedImages.Remove(image);
            Destroy(image);
        }
    }
}
