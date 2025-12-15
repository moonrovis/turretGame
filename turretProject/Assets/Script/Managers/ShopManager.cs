using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int currentIndex;
    public GameObject[] turretItems;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedTurret", 0);
        foreach(GameObject turret in turretItems) turret.SetActive(false);

        turretItems[currentIndex].SetActive(true);
    }

    public void changeNext()
    {
        turretItems[currentIndex].SetActive(false);

        currentIndex++;
        if(currentIndex == turretItems.Length) currentIndex = 0;

        turretItems[currentIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedTurret", currentIndex);
    }

    public void changePrevious()
    {
        
        turretItems[currentIndex].SetActive(false);

        currentIndex--;
        if(currentIndex < 0) currentIndex = turretItems.Length - 1;

        turretItems[currentIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedTurret", currentIndex);
    }
}
