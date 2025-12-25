using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject menuCanvas;

    public GameObject costPanel;
    public GameObject scorePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.SetActive(true);
            shopCanvas.SetActive(false);

            scorePanel.SetActive(true);
            costPanel.SetActive(false);
        }
    }
}
