using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class DartShooter : MonoBehaviour, IInteractable
{
    private Camera _camera;
    [SerializeField] private GameObject _dartPrefab;
    private GameObject _dart;
    void Start()
    {
        
        _camera = GetComponent<Camera>();

        if (_camera == null)
        {
            Debug.LogError("Camera component not found on the object.");
        }

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {

                    target.ReactToHit();
                    Debug.Log("Target hit");
                }
                else
                {
                    Debug.Log("Miss");
                }
            }
            else
            {
                Debug.Log("Miss");
            }
        }
    }
    

    public void Interact()
    {
        Debug.Log("Dart added to Inventory");
    }
}

