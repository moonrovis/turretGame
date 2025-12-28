using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentIndex;
    public GameObject[] turretItems;

    public TurretBlueprints[] turretModels;
    public Button buyButton;

    private CoinManager coinScript;

    private void Start()
    {
        coinScript = FindAnyObjectByType<CoinManager>();

        foreach(TurretBlueprints turrets in turretModels)
        {
            if(turrets.price == 0) turrets.isUnlocked = true;
            else turrets.isUnlocked = PlayerPrefs.GetInt(turrets.name, 0)==0? false: true;
        }


        currentIndex = PlayerPrefs.GetInt("SelectedTurret", 0);
        foreach(GameObject turret in turretItems) turret.SetActive(false);

        turretItems[currentIndex].SetActive(true);
        UpdateTurretUI();
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

        UpdateTurretUI();
    }

    public void changePrevious()
    {
        
        turretItems[currentIndex].SetActive(false);

        currentIndex--;
        if(currentIndex < 0) currentIndex = turretItems.Length - 1;

        turretItems[currentIndex].SetActive(true);

        UpdateTurretUI();
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
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }

    public void unclockTurret()
    {
        TurretBlueprints t = turretModels[currentIndex];
        int currentCoins = PlayerPrefs.GetInt("coin", 0);

        if (t.price <= currentCoins)
        {
            PlayerPrefs.SetInt(t.name, 1);
            PlayerPrefs.SetInt("SelectedTurret", currentIndex);
            t.isUnlocked = true;

            PlayerPrefs.SetInt("coin", currentCoins - t.price);
            coinScript.UpdateShopCoinText(); // Теперь текст обновится
        }
    }

    private void UpdateTurretUI()
    {
        TurretBlueprints t = turretModels[currentIndex];

        if (t.priceText != null)
        {
            t.priceText.text = t.price.ToString();
        }

        if (t.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("SelectedTurret", currentIndex);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
        }

        coinScript.UpdateShopCoinText();
    }

    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
