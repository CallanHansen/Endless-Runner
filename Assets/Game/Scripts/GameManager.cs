using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen = null;

    public static GameManager Instance;

    public float CurrentScore = 0;
    public float HighScore = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        CurrentScore += PlayerController.Instance.CurrentSpeed * Time.deltaTime;

        if (GameManager.Instance.CurrentScore >= GameManager.Instance.HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    public void GameOver() 
    {
        SaveSystem.SaveHighScore();
        gameOverScreen.SetActive(true);
        PlayerController.Instance.gameObject.SetActive(false);
        PlayerController.Instance.CurrentSpeed = 0;
        PlayerController.Instance.SpeedIncreaseMultiplier = 0;
    }

    void LoadHighScore() // Load the high score using SaveSystem
    {
        ScoreData data = SaveSystem.LoadScore();
        HighScore = data.HighScore;
    }
}
