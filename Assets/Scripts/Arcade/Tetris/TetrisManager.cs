using UnityEngine;
using UnityEngine.UI;

public class TetrisManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public RectTransform gridParent;

    public static float GridLeftEdge;
    public static float GridRightEdge;
    public const int width = 10;
    public const int height = 20;
    private Image[,] grid = new Image[width, height];


   
    void Start()
    {
        GenerateGrid();

        float halfWidth = gridParent.rect.width / 2f;
        GridLeftEdge = gridParent.localPosition.x - halfWidth;
        GridRightEdge = gridParent.localPosition.x + halfWidth;
    }
    

    void GenerateGrid()
    {
        for (int y = height - 1; y>= 0; y--)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject block = Instantiate(blockPrefab, gridParent);
                Image img = block.GetComponent<Image>();
                img.color = new Color(1, 1, 1, 0);
                grid[x,y] = img;

            }
        }
    }
    
    
}
