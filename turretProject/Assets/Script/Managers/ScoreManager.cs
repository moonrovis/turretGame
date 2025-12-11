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
            score += Time.deltaTime; 
            int seconds = Mathf.FloorToInt(score);
            T.text = seconds.ToString(); 

            PlayerPrefs.SetInt("score", seconds); 
        }
    }
}
