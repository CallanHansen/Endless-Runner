using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen = null;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);

        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver() 
    {
        gameOverScreen.SetActive(true);
        PlayerController.Instance.gameObject.SetActive(false);
        PlayerController.Instance.CurrentSpeed = 0;
        PlayerController.Instance.SpeedIncreaseMultiplier = 0;
    }
}
