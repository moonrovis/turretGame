using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] rocketPrefab; // Префаб ракеты
    public Transform turret; // Ссылка на турель
    public float spawnRadius = 10f; // Радиус окружности спавна
    public float minSpawnInterval = 1f; // Минимальный интервал спавна
    public float maxSpawnInterval = 3f; // Максимальный интервал спавна
    
    [Header("Spawn Area")]
    [Range(0f, 360f)]
    public float spawnArc = 360f; // Угол спавна (360 = полная окружность)
    
    private bool isSpawning = true;

    private Camera mainCamera;

    private Player playerScript;

    void Start()
    {
        mainCamera = Camera.main;

        if (rocketPrefab == null)
        {
            Debug.LogError("Rocket Prefab не назначен в SpawnManager!");
            return;
        }
        
        if (turret == null)
        {
            // Если турель не назначена, используем текущую позицию
            turret = transform;
            Debug.LogWarning("Turret не назначен, используется позиция SpawnManager");
        }
        
        playerScript = FindAnyObjectByType<Player>();

        StartCoroutine(SpawnRockets());
    }

    IEnumerator SpawnRockets()
    {
        while (isSpawning)
        {
            // Ждем случайное время
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            
            // Спавним ракету
            SpawnRocket();
        }
    }

    void SpawnRocket()
    {
        if (playerScript.isAlive)
        {           
            // Вычисляем случайную позицию на окружности
            Vector3 spawnPosition = GetRandomSpawnPosition();
            
            // Создаем ракету
            GameObject rocket = Instantiate(rocketPrefab[Random.Range(0, rocketPrefab.Length)], spawnPosition, Quaternion.identity);
            
            // Направляем ракету на турель (опционально)
            if (turret != null)
            {
                rocket.transform.LookAt(turret.position);
            }

            minSpawnInterval -= 0.05f;
            maxSpawnInterval -= 0.05f; 
            if(minSpawnInterval <= 0.5f) minSpawnInterval = 0.5f;
            if(maxSpawnInterval <= 0.75f) maxSpawnInterval = 0.75f;
            Debug.Log(minSpawnInterval);
            Debug.Log(maxSpawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Вычисляем случайный угол в пределах заданной дуги
        float randomAngle = Random.Range(0f, spawnArc);
        
        // Преобразуем угол в радианы
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        
        // Вычисляем позицию на окружности
        float x = Mathf.Cos(angleInRadians) * spawnRadius;
        float z = Mathf.Sin(angleInRadians) * spawnRadius;
        
        Vector3 spawnPosition = turret.position;
        return new Vector3(
        spawnPosition.x + x,
        1.6f,  // Фиксированная высота по Y
        spawnPosition.z + z);
    }

    // Метод для визуализации радиуса спавна в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = turret != null ? turret.position : transform.position;
        Gizmos.DrawWireSphere(center, spawnRadius);
        
        // Рисуем дугу спавна
        if (spawnArc < 360f)
        {
            DrawSpawnArcGizmo(center);
        }
    }

    void DrawSpawnArcGizmo(Vector3 center)
    {
        Gizmos.color = Color.yellow;
        int segments = 36;
        float angleStep = spawnArc / segments;
        
        Vector3 previousPoint = center + new Vector3(
            Mathf.Cos(0) * spawnRadius,
            0,
            Mathf.Sin(0) * spawnRadius
        );
        
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(
                Mathf.Cos(angle) * spawnRadius,
                0,
                Mathf.Sin(angle) * spawnRadius
            );
            
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
        
        // Рисуем линии от центра к краям дуги
        Gizmos.DrawLine(center, center + new Vector3(
            Mathf.Cos(0) * spawnRadius,
            0,
            Mathf.Sin(0) * spawnRadius
        ));
        
        Gizmos.DrawLine(center, center + new Vector3(
            Mathf.Cos(spawnArc * Mathf.Deg2Rad) * spawnRadius,
            0,
            Mathf.Sin(spawnArc * Mathf.Deg2Rad) * spawnRadius
        ));
    }

    // Методы для управления спавном из других скриптов
    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnRockets());
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    public void SetSpawnInterval(float min, float max)
    {
        minSpawnInterval = min;
        maxSpawnInterval = max;
    }
}