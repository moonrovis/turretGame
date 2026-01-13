using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;


public class AmmoManager : MonoBehaviour
{
    public int ammoCount = 50;
    public int ammoBox = 50;

    private float timer = 0f;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText;
    public Image reloadBar; // Ссылка на Image с типом Filled
    public GameObject reloadBarGameObject;

    private void Start()
    {
        ammoBox = PlayerPrefs.GetInt("ammoBox", ammoBox);
        ammoCount = ammoBox;

        reloadBarGameObject.SetActive(false);

        UpdateAmmoText();
        if (reloadBar != null)
        {
            reloadBar.gameObject.SetActive(false); // Скрыть при старте
        }
        PlayerPrefs.Save();
    }

    public void ReduceAmmo()
    {
        ammoCount--;
        UpdateAmmoText();

        if (ammoCount <= 0 && !isReloading)
        {
            isReloading = true;
            timer = 0f;

            // Показываем полосу перезарядки
            if (reloadBar != null)
            {
                reloadBar.gameObject.SetActive(true);
                reloadBar.fillAmount = 0f;
            }
        }
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadBarGameObject.SetActive(true);

            timer += Time.deltaTime;
            float fillProgress = timer / 1.6f;

            if (reloadBar != null)
            {
                reloadBar.fillAmount = Mathf.Clamp01(fillProgress);
            }

            if (timer >= 1.6f)
            {
                ReloadAmmo();
            }
        }
    }

    private void ReloadAmmo()
    {
        ammoCount = ammoBox;
        isReloading = false;
        reloadBarGameObject.SetActive(false);
        UpdateAmmoText();

        // Скрываем полосу перезарядки
        if (reloadBar != null)
        {
            reloadBar.gameObject.SetActive(false);
        }
    }

    public void UpdateAmmoText()
    {
        ammoText.text = ammoCount.ToString() + "/" + ammoBox.ToString();
    }

    public void RestartAmmoText()
    {
        UpdateAmmoText();
    }

    public void AddAmmoBox(int amount)
    {
        ammoBox += amount;
        PlayerPrefs.SetInt("ammoBox", ammoBox); // 🔧 Сохраняем новое значение
        PlayerPrefs.Save();
        UpdateAmmoText(); // обновляем текст
    }
}
