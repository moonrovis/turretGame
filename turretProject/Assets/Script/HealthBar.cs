using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    private Player playerScript;

    private Image healthImg;
    public float healhBar;

    private GameManager gameManagerScript;


    private void Start()
    {
        gameManagerScript = FindAnyObjectByType<GameManager>();
        healthImg = GetComponent<Image>();
        playerScript = FindAnyObjectByType<Player>();


    }

    private void Update()
    {
        if (!gameManagerScript.isPause)
        {            
            if (playerScript.isDamaged)
            {
                healhBar -= 0.25f;
                healthImg.fillAmount = healhBar;       
                playerScript.isDamaged = false;
            }
        }
    }
}