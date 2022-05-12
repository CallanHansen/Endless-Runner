[System.Serializable]
public class ScoreData
{
    public float HighScore;

    public ScoreData()
    {
        HighScore = GameManager.Instance.HighScore;
    }
}
