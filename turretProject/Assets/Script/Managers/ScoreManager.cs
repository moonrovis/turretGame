using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Player playerScript;
    private TextMeshProUGUI T;
    private float score;

    private void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        T = GetComponent<TextMeshProUGUI>();

        score = PlayerPrefs.GetInt("score", 0);
        score = 0;
    }

    private void Update()
    {
        if (playerScript.isAlive)
        {
            score += Time.deltaTime; // Накапливаем время
            int seconds = Mathf.FloorToInt(score); // Округляем вниз до целых секунд
            T.text = seconds.ToString(); // Показываем целое число секунд

            PlayerPrefs.SetInt("score", seconds); // Сохраняем результат
        }
    }
}
