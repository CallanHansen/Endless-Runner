using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private GameObject mainMenuScreen = null;


    public static GameManager Instance;

    public float CurrentScore = 0;
    public float HighScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuScreen.SetActive(true);
        LoadHighScore();
        gameOverScreen.SetActive(false);

        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

  


    void Update()
    {
        if(PlayerController.Instance.CanMove)
        { 
            CurrentScore += PlayerController.Instance.CurrentSpeed * Time.deltaTime;
        }
        
        if (GameManager.Instance.CurrentScore >= GameManager.Instance.HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    public void BeginGame()
    {
        PlayerController.Instance.CanMove = true;
        PlayerController.Instance.SpeedIncreaseMultiplier = PlayerController.Instance.BaseSpeedIncreaseMultiplier;
        PlayerController.Instance.CurrentSpeed = PlayerController.Instance.BaseSpeed;
    }

    public void GameOver() 
    {
        SaveSystem.SaveHighScore();
        gameOverScreen.SetActive(true);
        PlayerController.Instance.gameObject.SetActive(false);
        PlayerController.Instance.CanMove = false;
    }

    void LoadHighScore() // Load the high score using SaveSystem
    {
        ScoreData data = SaveSystem.LoadScore();
        HighScore = data.HighScore;
    }
}
