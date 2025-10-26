using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Needed if you display score in UI
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Singleton reference

    public TextMeshProUGUI scoreText;     // Assign in Inspector
    private int score = 0;

    private void Awake()
    {
        // Make sure only one ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}

