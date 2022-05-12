using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public static UIManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Update()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        currentScoreText.text = Mathf.RoundToInt(GameManager.Instance.CurrentScore).ToString();
        highScoreText.text = Mathf.RoundToInt(GameManager.Instance.HighScore).ToString();      
    }
}
