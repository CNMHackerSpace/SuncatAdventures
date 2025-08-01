using UnityEngine;

public class TetrominoController : MonoBehaviour
{
    public float fallSpeed = 1f;
    public TetrominoSpawner spawner;
    private float fallTimer = 0f;

    private bool isLanded = false;
    private float blockSize = 20f;
    private float minY = -180f; // bottom of 10x20 grid (20px * 9 rows down from origin)

    void Update()
    {
        if (isLanded) return;

        fallTimer += Time.deltaTime;

        if (fallTimer >= fallSpeed)
        {
            MoveDown();
            fallTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }
    }

    void Move(Vector2 direction)
    {
        transform.localPosition += new Vector3(direction.x * blockSize, 0f, 0f);
    }

    void MoveDown()
    {
        transform.localPosition += Vector3.down * blockSize;

        if (CheckIfLanded())
        {
            isLanded = true;
            // Eventually: Notify spawner to drop next piece
        }
    }

    void Rotate()
    {
        transform.Rotate(0f, 0f, -90f);
    }

    bool CheckIfLanded()
    {
        foreach (RectTransform block in transform)
        {
            float blockY = transform.localPosition.y + block.anchoredPosition.y;
            if (blockY <= minY)
            {
                isLanded = true;
                Invoke("NotifySpawner", 0.1f); 
                return true;
            }
        }
        return false;
    }


    void NotifySpawner()
    {
        if(spawner  != null)
        {
            spawner.SpawnNewTetromino();
        }
    }

    bool isMoveValid(Vector2 direction)
    {
        float blockSize = 20f;
        float minX = -90f;
        float maxX = 90f;

        foreach (RectTransform block in transform)
        {
            Vector2 newPos = (Vector2)transform.localPosition + block.anchoredPosition + (direction * blockSize);

            if (newPos.x < minX || newPos.x > maxX)
            {
                return false;
            }
        }
        return true;
    }




}
