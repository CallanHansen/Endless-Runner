using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MenuManagement : MonoBehaviour
{

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     
    }

   public void LoadScene(string _sceneToLoad)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
