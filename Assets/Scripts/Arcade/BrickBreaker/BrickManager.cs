using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject brickPrefab;
    public RectTransform spawnArea;

    public int columns = 8;
    public int rows = 3;
    public float spacing = 5f;

    void Start()
    {
        GenerateBricks();
    }

    public void GenerateBricks()
    {
        float brickWidth = brickPrefab.GetComponent<RectTransform>().rect.width;
        float brickHeight = brickPrefab.GetComponent<RectTransform>().rect.height;

        float startX = -((columns - 1) * (brickWidth + spacing)) / 2f;
        float startY = 100f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(
                    startX + col * (brickWidth + spacing),
                    startY - row * (brickHeight + spacing)
                );

                GameObject brick = Instantiate(brickPrefab, spawnArea);
                brick.GetComponent<RectTransform>().localPosition = position;
            }
        }
    }
}

