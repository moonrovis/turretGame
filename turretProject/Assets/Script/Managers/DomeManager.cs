using System.Collections;
using TMPro;
using UnityEngine;

public class domeManager : MonoBehaviour
{
    public bool isDomeActive;
    public GameObject dome;
    public TextMeshProUGUI timerText;

    public ParticleSystem spawnParticle;

    private void Start()
    {
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
            timeLeft -= Time.deltaTime;
            int seconds = Mathf.CeilToInt(timeLeft); // Округляем вверх: 5.1 → 6, 5.9 → 6, чтобы 1 появлялось до последней секунды
            if (timerText != null)
            {
                timerText.text = seconds.ToString();
            }
            yield return null; // Каждый кадр
        }

        dome.SetActive(false);
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false); // Скрыть текст после окончания
        }
    }
}
