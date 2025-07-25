using UnityEngine;

public class PongPlayerPaddle : MonoBehaviour
{
    public float speed = 400f;
    private RectTransform rectTransform;
    private float canvasHeight;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        float input = Input.GetAxis("Vertical");
        Vector3 pos = rectTransform.localPosition;
        pos.y += input * speed * Time.deltaTime;

        float halfHeight = rectTransform.rect.height / 2f;
        float maxY = canvasHeight / 2f - halfHeight;
        pos.y = Mathf.Clamp(pos.y, -maxY, maxY);

        rectTransform.localPosition = pos;
    }
}
