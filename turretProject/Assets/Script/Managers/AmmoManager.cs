using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int ammoCount = 50;
    public int ammoBox = 50;

    private float timer = 0;

    public TextMeshProUGUI ammoText;

    public void ReduceAmmo()
    {
        ammoCount--;
        ammoText.text = ammoCount.ToString() + "/" + ammoBox.ToString();
    }

    private void Update()
    {
        if(ammoCount <= 0)
        {
            timer += Time.deltaTime;
            if(timer >= 3)
            timer = 0;
            Restartammo();
        }
    }

    public void RestartAmmoText()
    {
        ammoText.text = ammoCount.ToString() + "/" + ammoBox.ToString();
    }

    private void Restartammo()
    {
        ammoCount = ammoBox;
        ammoText.text = ammoCount.ToString() + "/" + ammoBox.ToString();      
    }
}
