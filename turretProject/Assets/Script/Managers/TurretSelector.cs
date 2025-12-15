using UnityEngine;

public class TurretSelector : MonoBehaviour
{
    public int currentTurretIndex;
    public GameObject[] turrets;

    private void Start()
    {
        currentTurretIndex = PlayerPrefs.GetInt("SelectedTurret", 0);
        foreach(GameObject turret in turrets) turret.SetActive(false);

        turrets[currentTurretIndex].SetActive(true);
    }
}
