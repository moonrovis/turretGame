using UnityEngine;

public class Display : MonoBehaviour
{
    private Animator anim;
    public GameObject plus25;
    public GameObject min25;
    public GameObject min50;

    private Player playerScript;

    private void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        anim = GetComponentInChildren<Animator>();

        // Подписываемся на событие
        health.OnHealthIncreased += ShowHealthIncrease;
    }

    private void Update()
    {
        if (playerScript.isDamaged)
        {
            min25.SetActive(true);
            Invoke(nameof(resetRed), 1f);
            playerScript.isDamaged = false; // сброс флага, если нужно
        }

        if (playerScript.isDamagedBomb)
        {
            min50.SetActive(true);
            Invoke(nameof(resetRed50), 1f);
            playerScript.isDamagedBomb = false; // сброс флага, если нужно
        }
    }

    private void ShowHealthIncrease()
    {
        plus25.SetActive(true);
        Invoke(nameof(ResetInc), 1f);
    }

    private void resetRed()
    {
        min25.SetActive(false);
    }

    private void resetRed50()
    {
        min50.SetActive(false);
    }

    private void ResetInc()
    {
        plus25.SetActive(false);
    }

    private void OnDestroy()
    {
        // Отписываемся от события
        health.OnHealthIncreased -= ShowHealthIncrease;
    }
}
