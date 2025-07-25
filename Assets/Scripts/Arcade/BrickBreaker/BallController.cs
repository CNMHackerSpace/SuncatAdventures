using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(200f, 200f);
    public GameObject gameOverPanel;
    public RectTransform ballStartPoint;
    public PaddleController paddle; 

    private RectTransform rectTransform;
    private Vector2 velocity;
    private float canvasWidth;
    private float canvasHeight;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        velocity = initialVelocity;

        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
    }

    void Update()
    {
        // Move ball
        rectTransform.localPosition += (Vector3)(velocity * Time.deltaTime);

        Vector2 pos = rectTransform.localPosition;
        float halfSize = rectTransform.rect.width / 2f;

        // Bounce off left/right walls
        if (pos.x - halfSize < -canvasWidth / 2f || pos.x + halfSize > canvasWidth / 2f)
        {
            velocity.x *= -1;
        }

        // Bounce off top
        if (pos.y + halfSize > canvasHeight / 2f)
        {
            velocity.y *= -1;
        }

        // Ball fell off bottom
        if (pos.y - halfSize < -canvasHeight / 2f)
        {
            velocity = Vector2.zero;
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }


        CheckPaddleCollision();
        CheckBrickCollision();
        CheckForVictory();
    }

    public void ReflectY()
    {
        velocity.y *= -1;
    }

    void CheckPaddleCollision()
    {
        GameObject paddleObj = GameObject.Find("Paddle");
        if (paddleObj == null) return;

        RectTransform paddle = paddleObj.GetComponent<RectTransform>();

        // Convert local positions into world space
        Rect paddleRect = new Rect(
            paddle.position.x - paddle.rect.width / 2f,
            paddle.position.y - paddle.rect.height / 2f,
            paddle.rect.width,
            paddle.rect.height
        );

        Rect ballRect = new Rect(
            rectTransform.position.x - rectTransform.rect.width / 2f,
            rectTransform.position.y - rectTransform.rect.height / 2f,
            rectTransform.rect.width,
            rectTransform.rect.height
        );

        if (ballRect.Overlaps(paddleRect))
        {
            ReflectY();

            // Optional: Add slight horizontal bounce based on hit point
            float offset = rectTransform.position.x - paddle.position.x;
            velocity.x += offset * 5f;
        }
    }

    void CheckBrickCollision()
    {
        Brick[] bricks = GameObject.FindObjectsByType<Brick>(FindObjectsSortMode.None);

        foreach (Brick brick in bricks)
        {
            RectTransform brickRect = brick.GetComponent<RectTransform>();
            Rect rect = new Rect(
                brickRect.position.x - brickRect.rect.width / 2f,
                brickRect.position.y - brickRect.rect.height / 2f,
                brickRect.rect.width,
                brickRect.rect.height
            );

            Rect ballRect = new Rect(
                rectTransform.position.x - rectTransform.rect.width / 2f,
                rectTransform.position.y - rectTransform.rect.height / 2f,
                rectTransform.rect.width,
                rectTransform.rect.height
            );

            if (ballRect.Overlaps(rect))
            {
                brick.Break();
                ReflectY();
                break;
            }
        }
    }

    void CheckForVictory()
    {
        int brickCount = BrickBreakerGameManager.GetBrickCount();
        if (brickCount == 0 && gameOverPanel != null && velocity != Vector2.zero)
        {
            velocity = Vector2.zero;
            gameOverPanel.SetActive(true);
            Debug.Log("You win!");
        }
    }




}
