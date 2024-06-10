using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    bool isPaused = false;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;

    private void Awake()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        ManagePause();
    }

    public void SetIsPaused(bool _isPaused)
    {
        isPaused = _isPaused;
        ManagePause();
    }

    private void ManagePause()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            EnablePausePanel(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            EnablePausePanel(false);
            pauseButton.GetComponent<Button>().interactable = true;
        }
    }

    private void EnablePausePanel(bool enable)
    {
        if (pausePanel)
        {
            pausePanel.SetActive(enable);
        }
        else
        {
            return;
        }

        if(pauseButton)
        {
            pauseButton.GetComponent<Button>().interactable = false;
        }
    }
}
