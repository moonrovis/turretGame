using System.Collections;
using TMPro;
using UnityEngine;

public class domeManager : MonoBehaviour
{
    public bool isDomeActive;
    public GameObject dome;
    public TextMeshProUGUI timerText;

    private GameManager gameManagerScript;

    public ParticleSystem spawnParticle;

    private void Start()
    {
        gameManagerScript = FindAnyObjectByType<GameManager>();
        dome.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isDomeActive)
        {
            isDomeActive = false;
            StartCoroutine(domeReset(20f));
        }
    }

    private IEnumerator domeReset(float duration)
    {
        spawnParticle.Play();
        dome.SetActive(true);
        float timeLeft = duration;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }

        while (timeLeft > 0)
        {
            // Проверяем, что игра не на паузе
            if (!gameManagerScript.isPause)
            {
                timeLeft -= Time.deltaTime;
                int seconds = Mathf.CeilToInt(timeLeft);
                if (timerText != null)
                {
                    timerText.text = seconds.ToString();
                }
            }
            yield return null; // Ждём следующего кадра
        }

        dome.SetActive(false);
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }
}
