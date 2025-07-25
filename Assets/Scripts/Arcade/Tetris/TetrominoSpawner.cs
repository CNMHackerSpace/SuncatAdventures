using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    [Tooltip("Assign all 7 tetromino prefabs here")]
    public GameObject[] tetrominoPrefabs;

    [Tooltip("Parent transform for spawned tetrominoes (typically TetrisCanvas)")]
    public RectTransform spawnParent;

    public void SpawnNewTetromino()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length);
        GameObject newTetromino = Instantiate(tetrominoPrefabs[index], spawnParent);

        // Pass this spawner to the controller
        TetrominoController controller = newTetromino.GetComponent<TetrominoController>();
        if (controller != null)
        {
            controller.spawner = this;
        }
    }

}
