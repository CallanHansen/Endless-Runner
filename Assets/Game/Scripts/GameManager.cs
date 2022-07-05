using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private GameObject mainMenuScreen = null;

    public static GameManager Instance;

    public float CurrentScore = 0;
    public float HighScore = 0;

    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera mainMenuCamera = null;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera = null;

    void Start()
    {
        mainMenuCamera.Priority = 10;
        gameplayCamera.Priority = 1;

        mainMenuScreen.SetActive(true);   
        gameOverScreen.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        //SaveSystem.SaveHighScore(CurrentScore);
        HighScore = SaveSystem.LoadScore().HighScore;
    }

    void Update()
    {
        if(PlayerController.Instance.CanMove)
        { 
            CurrentScore += PlayerController.Instance.CurrentSpeed * Time.deltaTime;
        }
        
        if (CurrentScore >= HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    public void BeginGame()
    {
        mainMenuCamera.Priority = 1;
        gameplayCamera.Priority = 10;
        PlayerController.Instance.CanMove = true;
        PlayerController.Instance.SpeedIncreaseMultiplier = PlayerController.Instance.BaseSpeedIncreaseMultiplier;
        PlayerController.Instance.CurrentSpeed = PlayerController.Instance.BaseSpeed;
    }

    public void GameOver() 
    {
        if(CurrentScore >= HighScore)
        {
            SaveSystem.SaveHighScore(CurrentScore);
        }
        
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
