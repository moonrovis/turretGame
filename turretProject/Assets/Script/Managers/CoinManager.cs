using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coin = 0;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        coinText.text = coin.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) DeletePlayerPrefs();
    }

    public void UpdateCoinText()
    {
        coin++;
        PlayerPrefs.SetInt("coin", coin);
        coinText.text = coin.ToString();
        Debug.Log(coin);
    }

    public void UpdateShopCoinText()
    {
        coin = PlayerPrefs.GetInt("coin", 0); // Синхронизируем с PlayerPrefs
        coinText.text = coin.ToString();
    }

    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
