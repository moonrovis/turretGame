using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    private Player playerScript;

    public Image healthImg;
    public float healthBar;

    private GameManager gameManagerScript;


    private void Start()
    {
        gameManagerScript = FindAnyObjectByType<GameManager>();
        healthImg = GetComponent<Image>();
        playerScript = FindAnyObjectByType<Player>();
    }
}