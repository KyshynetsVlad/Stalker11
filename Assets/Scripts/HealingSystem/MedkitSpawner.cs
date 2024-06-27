using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit MedkitPrefab;
    [SerializeField] private int numberOfMedkitsToSpawn = 5;
    [SerializeField] private BoxCollider2D locationBounds; // BoxCollider2D, который охватывает всю локацию
    [SerializeField] private HealthInventory healthInventory;
    void Start()
    {
        SpawnMedkits();
    }

    void SpawnMedkits()
    {
        for (int i = 0; i < numberOfMedkitsToSpawn; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                MedkitPrefab.SetHealthInventory(healthInventory);
                Instantiate(MedkitPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int attempts = 0;
        while (attempts < 100)
        {
            attempts++;
            Vector3 randomPosition = GetRandomPositionWithinBounds();

            if (IsPositionValid(randomPosition))
            {
                return randomPosition;
            }
        }

        // Если не удалось найти валидное положение после 100 попыток, возвращаем Vector3.zero
        return Vector3.zero;
    }

    Vector3 GetRandomPositionWithinBounds()
    {
        float x = Random.Range(locationBounds.bounds.min.x, locationBounds.bounds.max.x);
        float y = Random.Range(locationBounds.bounds.min.y, locationBounds.bounds.max.y);
        return new Vector3(x, y, 0);
    }

    bool IsPositionValid(Vector3 position)
    {
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                EnemyController enemy = collider.GetComponent<EnemyController>();
                if (enemy != null && Vector3.Distance(position, enemy.transform.position) <= enemy.enemyVision)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
