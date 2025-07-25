using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 500f;
    private RectTransform rectTransform;
    private float canvasWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Vector3 pos = rectTransform.localPosition;
        pos.x += input * speed * Time.deltaTime;

        // Clamp to canvas width (minus half paddle width)
        float halfWidth = rectTransform.rect.width / 2f;
        float maxX = canvasWidth / 2f - halfWidth;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);

        rectTransform.localPosition = pos;
    }
}
