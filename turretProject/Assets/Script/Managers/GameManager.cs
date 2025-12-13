using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player playerScript;

    public GameObject deathCanvas;
    public GameObject pauseCanvas;

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
        
        if (!playerScript.isAlive) deathCanvas.SetActive(true);
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
        playerScript.isPause = true;
        pauseCanvas.SetActive(true);
    }

    public void resume()
    {
        playerScript.isPause = false;
        pauseCanvas.SetActive(false);
    }
}
