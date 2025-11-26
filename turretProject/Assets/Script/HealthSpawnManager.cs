using UnityEngine;

public class HealthSpawnManager : MonoBehaviour
{
    public Transform[] spawnPositions;

    public GameObject[] abilities;

    public float spawnInterval;

    private void Start()
    {
        InvokeRepeating("SpawnAbility", 30f, spawnInterval);
    }

    private void SpawnAbility()
    {
        Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        Instantiate(abilities[Random.Range(0, abilities.Length)], spawnPosition.position, Quaternion.identity);
    }
}
