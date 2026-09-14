using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        scoreText.text = "Score: " + ScoreCounter.finalScore;
        highScoreText.text = "High Score: " + HighScore.SCORE;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
