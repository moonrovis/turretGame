using UnityEngine;

public class CoinSpawnManager : HealthSpawnManager
{
    private float coinSpawnInterval = 40f;
    new private void Start()
    {
        spawnInterval = coinSpawnInterval;

        base.Start();
    }
}
