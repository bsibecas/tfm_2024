using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTasksCompleted : MonoBehaviour
{
    public GameObject Canvas;
    private GenerateRandomTasks generateRandomTasks;
    public TextMeshProUGUI textComponent;

    private void Start()
    {
        // Find the GenerateRandomTasks component in the scene
        generateRandomTasks = FindObjectOfType<GenerateRandomTasks>();

        if (generateRandomTasks == null)
        {
            Debug.LogError("GenerateRandomTasks component not found.");
            return;
        }
        if (textComponent == null)
        {
            Debug.LogError("Text component is not assigned in the inspector.");
            return;
        }
    }

    private void Update()
    {
        
        int tasksCompleted = generateRandomTasks.GetTasksCompleted();
        int numberOfTasks = generateRandomTasks.GetNumberOfTasks();

        if (tasksCompleted == numberOfTasks)
        {
            ShowTasksCanvas();
            UpdateTaskText(tasksCompleted, numberOfTasks);
        }


        Debug.Log("Tasks Completed: " + tasksCompleted);
    }
    public void UpdateTaskText(int tasksCompleted, int numberOfTasks)
    {
        textComponent.text = tasksCompleted + "/" + numberOfTasks + " COMPLETED TASKS";
    }

    public void ShowTasksCanvas()
    {
        if (Canvas != null)
        {
            Canvas.SetActive(true);
        }
    }

}
