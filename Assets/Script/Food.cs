using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject foodPrefab; 
    public Vector3 spawnAreaMin = new Vector3(-9, 0, -9);
    public Vector3 spawnAreaMax = new Vector3(9, 0, 9);
    public Transform ground;  // drag your Ground object here

    private float foodHeight;

    private void Start()
    {
        // Detect food height automatically (half the collider size)
        Collider col = GetComponent<Collider>();
        foodHeight = col != null ? col.bounds.size.y / 2f : 0.5f;

        // Make sure current food sits above ground
        if (ground != null)
        {
            Vector3 pos = transform.position;
            pos.y = ground.position.y + ground.localScale.y / 2f + foodHeight;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase score
            ScoreManager.Instance.AddScore(1);

            // Spawn a new food
            SpawnNewFood();

            // Destroy this one
            Destroy(gameObject);
        }
    }

    void SpawnNewFood()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        // Base height = ground top + half food height
        float yPos = ground.position.y + ground.localScale.y / 2f + foodHeight;

        Vector3 spawnPos = new Vector3(randomX, yPos, randomZ);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, ground ? ground.position.y : 0, (spawnAreaMin.z + spawnAreaMax.z) / 2);
        Vector3 size = new Vector3(spawnAreaMax.x - spawnAreaMin.x, 0.1f, spawnAreaMax.z - spawnAreaMin.z);
        Gizmos.DrawWireCube(center, size);
    }
}
