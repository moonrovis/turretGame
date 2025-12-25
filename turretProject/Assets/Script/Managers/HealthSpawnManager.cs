using UnityEngine;

public class HealthSpawnManager : MonoBehaviour
{
    public Transform[] spawnPositions;

    public GameObject[] abilities;

    private Player playerScript;

    public float spawnInterval;

    private GameManager gameManagerScript;

    protected void Start()
    {
        gameManagerScript = FindAnyObjectByType<GameManager>();

        playerScript = FindAnyObjectByType<Player>();

        InvokeRepeating("SpawnAbility", 30f, spawnInterval);
    }

    private void SpawnAbility()
    {
        if (playerScript.isAlive && !gameManagerScript.isPause)
        {         
            Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

            Instantiate(abilities[Random.Range(0, abilities.Length)], spawnPosition.position, Quaternion.identity);
        }
    }
}
