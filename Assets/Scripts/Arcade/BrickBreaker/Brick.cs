using UnityEngine;

public class Brick : MonoBehaviour
{
    public void Break()
    {
        Destroy(gameObject);
    }
}
