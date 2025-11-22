using UnityEngine;

public class Display : MonoBehaviour
{
    private Animator anim;
    private float timer;
    public GameObject incHealth;
    public GameObject redHealth;

    private Player playerScript;
    private health healthScript;

    private void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        anim = GetComponentInChildren<Animator>();
        healthScript = FindAnyObjectByType<health>();
    }

    public void Update()
    {
        if (playerScript.isDamaged)
        {
            redHealth.SetActive(true);
            Invoke(nameof(resetRed), 0.5f);
        }
        if (healthScript.healthInc)
        {
            incHealth.SetActive(true);
            Invoke(nameof(ResetInc), 0.5f);
        }

    }

    private void resetRed()
    {
        redHealth.SetActive(false);
    }

    private void ResetInc()
    {
        incHealth.SetActive(false);
    }
}
