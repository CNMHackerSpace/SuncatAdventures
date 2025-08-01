using UnityEngine;

public class PongBall : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(300f, 300f);
    private Vector2 velocity;
    private RectTransform rectTransform;
    private float canvasWidth, canvasHeight;

    public Vector2 Velocity => velocity;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Randomize initial launch direction
        float xDir = Random.value < 0.5f ? -1f : 1f;
        float yDir = Random.Range(-0.7f, 0.7f);
        Vector2 direction = new Vector2(xDir, yDir).normalized;
        velocity = direction * initialVelocity.magnitude;

        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
    }


    void Update()
    {
        rectTransform.localPosition += (Vector3)(velocity * Time.deltaTime);

        Vector2 pos = rectTransform.localPosition;
        float halfSize = rectTransform.rect.width / 2f;

        // Bounce off top and bottom walls
        if (pos.y + halfSize > canvasHeight / 2f || pos.y - halfSize < -canvasHeight / 2f)
        {
            velocity.y *= -1;
        }

        // Player missed (ball goes past left wall)
        if (pos.x < -canvasWidth / 2f)
        {
            velocity = Vector2.zero;
            PongGameManager.Instance.AIScores();
            Destroy(gameObject);
            return;
        }

        // AI missed (ball goes past right wall)
        if (pos.x + halfSize > canvasWidth / 2f)
        {
            velocity = Vector2.zero;
            PongGameManager.Instance.PlayerScores();
            Destroy(gameObject);
            return;
        }

        CheckPaddleBounce();
    }

    void CheckPaddleBounce()
    {
        CheckCollisionWithPaddle("PlayerPaddle");
        CheckCollisionWithPaddle("AIPaddle");
    }

    void CheckCollisionWithPaddle(string paddleName)
    {
        GameObject paddleObj = GameObject.Find(paddleName);
        if (paddleObj == null) return;

        RectTransform paddle = paddleObj.GetComponent<RectTransform>();

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
            velocity.x *= -1;

            // Optional: add vertical angle based on where the ball hits
            float offset = rectTransform.position.y - paddle.position.y;
            velocity.y += offset * 5f;
        }
    }
}
