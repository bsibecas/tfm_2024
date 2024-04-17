using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    //DECLARATIONS
    public static GameSceneManager INSTANCE;

    public enum GameScenes
    { 
        MainMenu = 0,
        Level = 1,
        LevelClear = 2,
        GameOver = 3,
    }


    //INTERNAL FUNCTIONS
    private void Awake()
    {
        MakeInstanceSingleton();
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

        DontDestroyOnLoad(gameObject);
    }


    //EXTERNAL FUNCTIONS
    public static void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void QuitGame()
    {
        Application.Quit();

         #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
         #endif
    }
}
