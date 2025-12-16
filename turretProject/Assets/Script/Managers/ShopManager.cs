using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentIndex;
    public GameObject[] turretItems;

    public TurretBlueprints[] turretModels;
    public Button buyButton;

    private void Start()
    {
        foreach(TurretBlueprints turrets in turretModels)
        {
            if(turrets.price == 0) turrets.isUnlocked = true;
            else turrets.isUnlocked = PlayerPrefs.GetInt(turrets.name, 0)==0? false: true;
        }


        currentIndex = PlayerPrefs.GetInt("SelectedTurret", 0);
        foreach(GameObject turret in turretItems) turret.SetActive(false);

        turretItems[currentIndex].SetActive(true);
    }

    private void Update()
    {
        updateUI();
        if(Input.GetKeyDown(KeyCode.P)) DeletePlayerPrefs();
    }

    public void changeNext()
    {
        turretItems[currentIndex].SetActive(false);

        currentIndex++;
        if(currentIndex == turretItems.Length) currentIndex = 0;

        turretItems[currentIndex].SetActive(true);

        TurretBlueprints t = turretModels[currentIndex];
        if(!t.isUnlocked) return;

        PlayerPrefs.SetInt("SelectedTurret", currentIndex);
    }

    public void changePrevious()
    {
        
        turretItems[currentIndex].SetActive(false);

        currentIndex--;
        if(currentIndex < 0) currentIndex = turretItems.Length - 1;

        turretItems[currentIndex].SetActive(true);

        TurretBlueprints t = turretModels[currentIndex];
        if(!t.isUnlocked) return;

        PlayerPrefs.SetInt("SelectedTurret", currentIndex);
    }

    private void updateUI()
    {
        TurretBlueprints t = turretModels[currentIndex];
        if (t.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            if(t.price <= PlayerPrefs.GetInt("coin", 0))
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }

    public void unclockTurret()
    {
        TurretBlueprints t = turretModels[currentIndex];
        if(t.price <= PlayerPrefs.GetInt("coin", 0))
        {       
            PlayerPrefs.SetInt(t.name, 1);
            PlayerPrefs.SetInt("SelectedTurret", currentIndex);
            t.isUnlocked = true;
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) - t.price);
        }
    }

    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
