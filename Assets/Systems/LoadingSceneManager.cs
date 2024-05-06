using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    //DECLARATIONS
    public static LoadingSceneManager INSTANCE;

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
