using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    //DECLARATIONS
    private static GameSceneManager INSTANCE;

    public enum GameScenes
    { 
        MainMenu = 0,
        Level = 1,
        LevelClear = 2,
        GameOver = 3,
    }

    private TimeSystem timeSystem;

    //INTERNAL FUNCTIONS
    private void Awake()
    {
        MakeInstanceSingleton();
        timeSystem = (TimeSystem)FindAnyObjectByType(typeof(TimeSystem));
    }

    private void MakeInstanceSingleton()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != (int)GameScenes.Level)
        {
            return;
        }

        if(StressManager.GetStressAmount() == 1) 
        {
            GameManager.firedByStress = true;
            LoadScene((int)GameScenes.GameOver);
        }

        if (!timeSystem)
        {
            return;
        }

        if (timeSystem.GetCastingTime() < 0)
        {
            if (GameManager.satisfiedClients < GameManager.minClients)
            {
                GameManager.firedByStress = false;
                LoadScene((int)GameScenes.GameOver);
            }
            else
            {
                LoadScene((int)GameScenes.LevelClear);
            }
        }
    }


    //EXTERNAL FUNCTIONS
    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();

         #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
         #endif
    }
}
