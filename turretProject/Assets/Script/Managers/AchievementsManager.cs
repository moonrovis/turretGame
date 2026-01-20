// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using YG;
// using PlayerPrefs = RedefineYG.PlayerPrefs;

// public class AchievementsManager : MonoBehaviour
// {
//     public Image checkMark;
//     public TextMeshProUGUI killCount;
//     public TextMeshProUGUI lifeCount;

//     private CoinManager coinManagerScript;

//     private void Start()
//     {
//         coinManagerScript = FindAnyObjectByType<CoinManager>();

//         int kills = PlayerPrefs.GetInt("killCount", 0);
//         killCount.text = kills.ToString() + "/1000";

//         // Проверяем, достигнуто ли 1000 убийств и была ли уже выдана награда
//         bool rewardGiven = PlayerPrefs.GetInt("achievement_1000kills_rewarded", 0) == 1;

//         if (kills >= 10 && !rewardGiven)
//         {
//             // Выдаём 1000 монет один раз
//             coinManagerScript.coin += 10;
//             PlayerPrefs.SetInt("coin", coinManagerScript.coin);
//             PlayerPrefs.SetInt("achievement_1000kills_rewarded", 1); // Помечаем, что награда получена
//             PlayerPrefs.Save();

//             // Обновляем текст монет
//             coinManagerScript.UpdateCoinText(); // или можно вызвать UpdateShopCoinText()

//             Debug.Log("Награда за 1000 убийств получена! +1000 монет");
//         }

//         if (kills >= 10)
//         {
//             coinManagerScript.AchievementCoinAdd();
//             checkMark.gameObject.SetActive(true);
//             killCount.gameObject.SetActive(false);
//         }
//     }

//     public void UpdateKillCount()
//     {
//         int kills = PlayerPrefs.GetInt("killCount", 0);
//         killCount.text = kills.ToString() + "/1000";

//         bool rewardGiven = PlayerPrefs.GetInt("achievement_1000kills_rewarded", 0) == 1;

//         if (kills >= 1000 && !rewardGiven)
//         {
//             coinManagerScript.coin += 1000;
//             PlayerPrefs.SetInt("coin", coinManagerScript.coin);
//             PlayerPrefs.SetInt("achievement_1000kills_rewarded", 1);
//             PlayerPrefs.Save();
//             coinManagerScript.UpdateCoinText();
//             Debug.Log("Награда за 1000 убийств получена! +1000 монет");
//         }
//     }
// }
