using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player playerScript;

    public GameObject deathCanvas;
    public GameObject pauseCanvas;

    public bool isPause = false;

    void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();   
        }
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
    }

    public void resume()
    {
        isPause = false;
        pauseCanvas.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        deathCanvas.SetActive(true);
    }
}
