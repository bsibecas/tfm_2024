using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowTasksCompleted : MonoBehaviour
{
    private GenerateRandomTasks generateRandomTasks;
    private TimeSystem timeSystem;

    public TextMeshProUGUI textComponent;
    public GameObject CanvasTasks;
    public GameObject CanvasEnd;


    private void Start()
    {
        timeSystem = FindObjectOfType<TimeSystem>();

        
        if (textComponent == null)
        {
            Debug.LogError("Text component is not assigned in the inspector.");
            return;
        }
    }

    private void Update()
    {
        float time = timeSystem.GetCastingTime();

        if (time <= 0)
        {
            ShowTasksCanvas(CanvasTasks);
        }

        if (CanvasTasks.activeSelf == true)
        {
            generateRandomTasks = FindObjectOfType<GenerateRandomTasks>();
            if (generateRandomTasks == null)
            {
                Debug.LogError("GenerateRandomTasks component not found.");
                return;
            }

            int tasksCompleted = generateRandomTasks.GetTasksCompleted();
            int numberOfTasks = generateRandomTasks.GetNumberOfTasks();

            if (tasksCompleted == numberOfTasks || time <= 0)
            {
                UpdateTaskText(tasksCompleted, numberOfTasks);
                ShowTasksCanvas(CanvasEnd);
                CanvasTasks.SetActive(false);
            }
        }

    }
    public void UpdateTaskText(int tasksCompleted, int numberOfTasks)
    {
        textComponent.text = tasksCompleted + "/" + numberOfTasks + " COMPLETED TASKS";
    }

    public void ShowTasksCanvas(GameObject Canvas)
    {
        SceneManager.LoadScene("2-LevelClear");

        if (Canvas != null)
        {
            Canvas.SetActive(true);
        }
    }

}
