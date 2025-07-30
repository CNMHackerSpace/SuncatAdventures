using UnityEngine;

public class KeyboardSelectableMemoryTile : MonoBehaviour
{
    private Camera mainCamera;
    private MemoryTileMAG tile;

    void Start()
    {
        mainCamera = Camera.main;
        tile = GetComponent<MemoryTileMAG>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
#pragma warning restore IDE0090 // Use 'new(...)'
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.gameObject == gameObject && tile != null)
                {
                    tile.ProcessSelection();
                }
            }
        }
    }
}
