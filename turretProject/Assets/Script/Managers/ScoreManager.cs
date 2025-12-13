using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Player playerScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private float score;
    private int bestScore;

    private void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        score = 0;

        // Показываем лучший счёт при старте
        bestScoreText.text = bestScore.ToString();
    }

    private void Update()
    {
        if (playerScript.isAlive && !playerScript.isPause)
        {
            score += Time.deltaTime;
            int seconds = Mathf.FloorToInt(score);
            scoreText.text = seconds.ToString();

            if (seconds > bestScore) // Сравниваем с текущим лучшим
            {
                bestScore = seconds; // Обновляем переменную
                PlayerPrefs.SetInt("bestScore", bestScore); // Сохраняем
                bestScoreText.text = bestScore.ToString(); // Обновляем текст
            }
        }
    }
}
