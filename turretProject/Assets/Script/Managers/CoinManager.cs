using System;
using TMPro;
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

    public void UpdateCoinText()
    {
        coin++;
        PlayerPrefs.SetInt("coin", coin);
        coinText.text = coin.ToString();
        Debug.Log(coin);
    }
}
