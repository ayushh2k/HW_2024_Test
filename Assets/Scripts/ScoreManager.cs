using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; 
    public TextMeshProUGUI scoreText; 

    private void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText(); 
        Debug.Log("Score increased: " + score);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
