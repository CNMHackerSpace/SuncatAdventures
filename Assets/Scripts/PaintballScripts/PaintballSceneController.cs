using UnityEngine;

public class PaintballSceneController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}