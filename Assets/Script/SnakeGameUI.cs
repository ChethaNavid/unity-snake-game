using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeGameUI : MonoBehaviour
{
    public static SnakeGameUI Instance;

    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
