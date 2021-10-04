using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private int _currentStage = 0;
    public int BestScore { get; private set; }
    public int CurentScore { get; private set; }

    private void Awake()
    {
        SetGameManager();
        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void SetGameManager()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);
    }

    public void GoNextLevel()
    {
        _currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(_currentStage);
    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        // Show ads
        gameManager.CurentScore = 0;
        FindObjectOfType<BallController>().ResetBall();
        // Reload stage
        FindObjectOfType<HelixController>().LoadStage(_currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        CurentScore += scoreToAdd;
        if (CurentScore > BestScore)
        {
            BestScore = CurentScore;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
    }
}