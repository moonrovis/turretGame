using System;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coin = 0;

    public void UpdateCoinText()
    {
        coin++;
        coinText.text = coin.ToString();
        Debug.Log(coin);
    }
}
