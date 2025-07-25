using UnityEngine;

public class PongAIPaddle : MonoBehaviour
{
    public RectTransform ball;         // Set by PongGameManager when ball is spawned
    public float speed = 300f;

    private RectTransform rectTransform;
    private float canvasHeight;

    private PongBall ballScript;       // Gets updated dynamically

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (ball == null) return;

        // Reacquire ball script if it's missing or the ball has changed
        if (ballScript == null || ballScript.gameObject != ball.gameObject)
        {
            ballScript = ball.GetComponent<PongBall>();
        }

        // Only track if the ball is moving toward the AI paddle
        if (ballScript == null || ballScript.Velocity.x <= 0f) return;

        Vector3 pos = rectTransform.localPosition;
        float direction = ball.localPosition.y - pos.y;

        if (Mathf.Abs(direction) > 5f)
        {
            pos.y += Mathf.Sign(direction) * speed * Time.deltaTime;
        }

        float halfHeight = rectTransform.rect.height / 2f;
        float maxY = canvasHeight / 2f - halfHeight;
        pos.y = Mathf.Clamp(pos.y, -maxY, maxY);

        rectTransform.localPosition = pos;
    }
}
