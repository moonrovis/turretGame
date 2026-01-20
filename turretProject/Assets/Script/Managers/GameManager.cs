using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class GameManager : MonoBehaviour
{
    private Player playerScript;
    private bar barScript;
    private AmmoManager ammoManagerScript;

    public GameObject deathCanvas;
    public GameObject pauseCanvas;
    public GameObject mobileCanvas;

    private string rewardId = "live";

    public bool isPause = false;
    public bool isRewarded = false;

    private float timer;
    public TextMeshProUGUI adTimer;
    public GameObject ad;

    void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        barScript = FindAnyObjectByType<bar>();
        ammoManagerScript = FindAnyObjectByType<AmmoManager>();
        YG2.InterstitialAdvShow();
        ad.SetActive(false);
   }

    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();   
        }

            timer += Time.deltaTime;
            if (timer >= 88)
            {
                ad.SetActive(true);
                adTimer.text = 2.ToString();
            }
            if (timer >= 89) adTimer.text = 1.ToString();
            if (timer >= 90)
            {
                pauseCanvas.SetActive(true);
                isPause = true;
                YG2.InterstitialAdvShow();
                timer = 0;
                ad.SetActive(false);
            }

            Debug.Log(timer);
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void pause()
    {    
        isPause = true;
        pauseCanvas.SetActive(true);
        mobileCanvas.SetActive(false);
    }

    public void resume()
    {
        isPause = false;
        pauseCanvas.SetActive(false);


        mobileCanvas.SetActive(playerScript.isMobile);
        
    }

    public void OnPlayerDeath()
    {
        deathCanvas.SetActive(true);

        if (playerScript.useMobileControl)
        {
            mobileCanvas.SetActive(false);
        }
    }

    public void continueReward()
    {
        YG2.RewardedAdvShow(rewardId,() =>
        {
            isRewarded = true;
            deathCanvas.SetActive(false);

            barScript.healthBar = 1f;
            barScript.healthImg.fillAmount = barScript.healthBar;   
        });
    }

    public void raiseAmmoReward()
    {
        YG2.RewardedAdvShow(rewardId,() =>
        {
            ammoManagerScript.AddAmmoBox(20);
        });
    }
}
