using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;    // The NormalFireBoss prefab
    [SerializeField] private Transform spawnPoint;     // Where to spawn the boss
    [SerializeField] private string triggeringTag = "Player";  // What tag triggers the spawn

    private bool hasSpawned = false; // Prevent spawning multiple times

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag(triggeringTag))
        {
            if (bossPrefab != null && spawnPoint != null)
            {
                Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
                hasSpawned = true;
            }
            else
            {
                Debug.LogWarning("BossTriggerSpawner: Boss prefab or spawn point not assigned.");
            }
        }
    }
}
